using App.AthenticationService.Models;
using App.AthenticationService.Requests;
using App.AthenticationService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.AthenticationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Produces("application/json")]
    public class JwtController : Controller
    {
        private readonly IAuthService _jWTService;

        public JwtController(IAuthService jWTService)
        {
            _jWTService = jWTService;
        }

        [HttpGet("GerarToken")]
        [ProducesResponseType(550)]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GerarToken([FromBody]GerarTokenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Email))
                    return BadRequest(request);

                var model = new JWTContainerModel(request.Nome, request.Email);

                var tokenGenerated = _jWTService.GenerateToken(model);

                return Ok(tokenGenerated);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar token: {ex.Message}");
            }
        }

        [HttpGet("ObterTokenClaims")]
        [ProducesResponseType(550)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Claim>))]
        public IActionResult ObterTokenClaims([FromBody]ObterTokenClaimsRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Token))
                    return BadRequest(request);

                var claims = _jWTService.GetTokenClaims(request.Token);

                if (claims == null)
                    return NotFound();

                return Ok(claims);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro ao gerar token: {ex.Message}");
            }
        }

        [HttpGet("ValidarToken")]
        [ProducesResponseType(550)]
        [ProducesResponseType(200, Type = typeof(bool))]
        public IActionResult ValidarToken([FromBody]ValidarTokenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Token))
                    return BadRequest(request);

                var isValid = _jWTService.IsTokenValid(request.Token);

                if (!isValid) return NotFound();
                
                return Ok(isValid);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar token: {ex.Message}");
            }
        }
    }
}
