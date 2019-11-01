using App.AthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.AthenticationService.Services
{
    public interface IAuthService
    {
        string SecretKey { get; set; }
        string GenerateToken(IAuthContainerModel model);
        bool IsTokenValid(string token);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}
