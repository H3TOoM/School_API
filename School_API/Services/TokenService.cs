using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using School_API.Models;
using School_API.Services.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School_API.Services
{
    public class TokenService(JwtOptions jwtOptions, UserManager<User> userManager) : ITokenService
    {
        // Method to create JWT token for the given user
        public async Task<string> CreateToken(User user)
        {
            // Get all roles assigned to the user
            var userRoles = await userManager.GetRolesAsync(user);

            // Create a list of claims (data stored inside the token)
            var authClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()), // Custom claim: User ID
                new Claim(ClaimTypes.Name, user.Name), // User's full name
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
                new Claim(JwtRegisteredClaimNames.Email, user.Email) // User's email
            };

            // Add all user roles as claims
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create signing key from the secret in jwtOptions
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SigningKey)
            );

            // Create the JWT token object
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,             // Token issuer
                audience: jwtOptions.Audience,         // Token audience
                expires: DateTime.UtcNow.AddHours(3),  // Token expiration (3 hours)
                claims: authClaims,                    // Claims list
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256 // Encryption algorithm
                )
            );

            // Convert the token object to a string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
