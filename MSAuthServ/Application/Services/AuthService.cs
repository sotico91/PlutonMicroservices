using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MSAuthServ.Application.Interfaces;

namespace MSAuthServ.Application.Services
{
	public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;

        public AuthService(IAuthRepository authRepository, string issuer, string audience, string secretKey)
        {
            _authRepository = authRepository;
            _issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            _audience = audience ?? throw new ArgumentNullException(nameof(audience));
            _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            // Validar las credenciales del usuario
            var user = await _authRepository.ValidateUserAsync(username, password);
            if (user == null)
            {
                return null;
            }

            // Generar el token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var audiences = _audience.Split(',');
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                 }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = _issuer,
                Audience = audiences[0],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}