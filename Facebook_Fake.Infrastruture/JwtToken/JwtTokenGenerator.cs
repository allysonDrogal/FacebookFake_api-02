
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Facebook_Fake.Infrastruture.JwtToken
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly string _secretKey;
        private readonly int _expiryMinutes;

        public JwtTokenGenerator(string secretKey, int expiryMinutes = 100)
        {
            _secretKey = secretKey;
            _expiryMinutes = expiryMinutes;
        }

        public string GenerateToken(Guid userId, string email, string name)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Name, name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Facebook_Fake",  // Quem emitiu o token
                audience: "Facebook_Fake_Users",   // Para quem o token é destinado
                claims: claims,  // Informações do usuário
                expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
                signingCredentials: creds  // Assinatura digital
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
