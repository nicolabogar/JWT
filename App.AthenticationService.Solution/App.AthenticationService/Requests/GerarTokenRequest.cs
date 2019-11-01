using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.AthenticationService.Requests
{
    public class GerarTokenRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
