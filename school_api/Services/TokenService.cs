﻿using Microsoft.IdentityModel.Tokens;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace school_api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService( IConfiguration configuration )
        {
            _configuration = configuration;
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
    }
}
