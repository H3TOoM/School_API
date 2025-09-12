using Microsoft.IdentityModel.Tokens;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace school_api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IMainRepoistory<RefreshToken> _refreshTokenRepository;
        private readonly IMainRepoistory<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(
            IConfiguration configuration,
            IMainRepoistory<RefreshToken> refreshTokenRepository,
            IMainRepoistory<User> userRepository,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<string> GenerateTokenAsync( UserReadDto user )
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey( System.Text.Encoding.UTF8.GetBytes( _configuration["JWT:Key"] ) );
            var creds = new SigningCredentials( key, SecurityAlgorithms.HmacSha256 );
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes( 30 ),
                signingCredentials: creds
            );

            return Task.FromResult( new JwtSecurityTokenHandler().WriteToken( token ) );
        }

        public async Task<TokenResponseDto> GenerateTokenPairAsync(UserReadDto user)
        {
            var accessToken = await GenerateTokenAsync(user);
            var refreshToken = GenerateRefreshToken();

            // Save refresh token to database
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7), // 7 days
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.CreateAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                User = user
            };
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var tokenEntity = (await _refreshTokenRepository.GetAllAsync())
                .FirstOrDefault(t => t.Token == refreshToken && !t.IsRevoked && t.ExpiresAt > DateTime.UtcNow);

            if (tokenEntity == null)
                return null;

            var user = await _userRepository.GetByIdAsync(tokenEntity.UserId);
            if (user == null)
                return null;

            // Revoke old refresh token
            tokenEntity.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(tokenEntity.Id, tokenEntity);

            // Generate new token pair
            var userDto = new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };

            return await GenerateTokenPairAsync(userDto);
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var tokenEntity = (await _refreshTokenRepository.GetAllAsync())
                .FirstOrDefault(t => t.Token == refreshToken);

            if (tokenEntity == null)
                return false;

            tokenEntity.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(tokenEntity.Id, tokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
