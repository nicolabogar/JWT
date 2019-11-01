using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.AthenticationService.Models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public JWTContainerModel(string nome, string email)
        {
            this.Claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Email, email)
            };
        }

        public string SecretKey { get { return "i_sQhhDgpSsQlb11HDxedhHcIIhC55U-R-LflVdqA7w"; }  }
        public string SecurityAlgorithm { get { return SecurityAlgorithms.HmacSha256Signature; } }
        public int ExpireMinutes { get { return 10880; } }
        public Claim[] Claims { get; set; }
    }
}
