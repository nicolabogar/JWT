using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.AthenticationService.Models;
using Microsoft.IdentityModel.Tokens;

namespace App.AthenticationService.Services
{
    public class JWTService : IAuthService
    {
        public JWTService(string secretKey)
        {
            this.SecretKey = secretKey;
        }

        public string SecretKey { get; set; }

        public string GenerateToken(IAuthContainerModel model)
        {
            if (model == null || model.Claims == null || model.Claims.Length == 0)
                throw new Exception("Argumentos da model não são válidos para geração do token.");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(model.Claims),
                Expires = DateTime.UtcNow.AddMinutes((int)model.ExpireMinutes),
                SigningCredentials = new SigningCredentials(GetSimmetricSecurityKey(), model.SecurityAlgorithm)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new Exception("Claims é nulo ou vázio.");

            var tokenValidationParameters = GetTokenValidationParameters();
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                return tokenValid.Claims;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new Exception("O token informado é nulo ou vazio.");

            var tokenValidationParameters = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal tokenValid = tokenValidationParameters.ValidateToken(token, this.GetTokenValidationParameters(), out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private SecurityKey GetSimmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(this.SecretKey);

            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var tokenParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSimmetricSecurityKey()
            };
            return tokenParameters;
        }
    }
}
