using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AiChatSystem.Helpers
{
    public  class TokenValidationHelpers
    {
        private readonly  IConfiguration _configuration;
        public TokenValidationHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenValidationAndRead(string token){
            var secretKey = _configuration.GetSection("Jwt:SecretKey").Value;
            var secretKeyEncoder = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var validateParams = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = secretKeyEncoder,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };
            var validate = new JwtSecurityTokenHandler().ValidateToken(token, validateParams, out SecurityToken validatedToken);
            var userId = validate.Claims.FirstOrDefault(x => x.Type == "TenantId")?.Value;
            return userId;
        }
    }
}
