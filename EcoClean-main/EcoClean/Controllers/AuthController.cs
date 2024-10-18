using EcoClean.Models;
using EcoClean.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcoClean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        // Injeção de dependência do AuthService via construtor
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UserModel user)
        {
            // Autentica o usuário com o serviço AuthService
            var authenticatedUser = _authService.Authenticate(user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized(); // Retorna 401 se não autenticado
            }

            // Gera o token JWT para o usuário autenticado
            var token = GenerateJwtToken(authenticatedUser);
            return Ok(new { Token = token });
        }

        // Método para gerar o token JWT
        private string GenerateJwtToken(UserModel user)
        {
            byte[] secret = Encoding.ASCII.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi");

            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Garante que os valores não serão nulos usando o operador '??'
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username ?? "Anonymous"), // Valor padrão
                new Claim(ClaimTypes.Role, user.Role ?? "User"),           // Valor padrão
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID único
                new Claim(JwtRegisteredClaimNames.Iss, "ecoclean"), // Emissor
                new Claim(JwtRegisteredClaimNames.Aud, "https://minha-api.com") // Audience
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30), // Validade
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
