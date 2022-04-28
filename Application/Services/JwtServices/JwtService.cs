using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.JwtServices
{
    public class JwtService :IJwtService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly SiteSettings _siteSettings;
        public JwtService(IOptionsSnapshot<SiteSettings> siteSetting, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _siteSettings = siteSetting.Value;
        }
        public async Task<string> GenerateAsync(User user)
        {
            var secretKey = _siteSettings.JwtSettings.SecretKey;
            var encryptionkey = _siteSettings.JwtSettings.EncryptKey;
            var claims = await GetClaimsAsync(user);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),SecurityAlgorithms.HmacSha256Signature);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionkey)), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            var descriptor = new SecurityTokenDescriptor()
            {
                Audience = _siteSettings.JwtSettings.Audience,
                Issuer = _siteSettings.JwtSettings.Issuer,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.ExpirationMinutes),
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.NotBeforeMinutes),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials
            };
     
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            var jwt = tokenHandler.WriteToken(securityToken);
            return jwt;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            return result.Claims;
           
        }
    }

    
}
