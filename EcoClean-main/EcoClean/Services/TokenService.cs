using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EcoClean.Services
{
    public class TokenService
    {
        private const string SecretKey = "chave-super-secreta-para-teste"; 

        public string GenerateToken(string userId)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new Claim(JwtRegisteredClaimNames.Iss, "https://meu-servidor-autenticacao.com"), 
                new Claim(JwtRegisteredClaimNames.Aud, "https://minha-api.com") 
            };

            var token = new JwtSecurityToken(
                issuer: "https://meu-servidor-autenticacao.com",
                audience: "https://minha-api.com",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Token expira em 30 minutos
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
