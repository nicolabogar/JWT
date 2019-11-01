using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.AthenticationService.Models
{
    public interface IAuthContainerModel
    {
        string SecretKey { get; }
        string SecurityAlgorithm { get; }
        int ExpireMinutes { get; }
        Claim[] Claims { get; set; }
    }
}
