using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                Employee employee = _authService.LogIn(email, password);
                var token = _tokenService.GenerateToken(employee);
                return Ok(new { Token = token });
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
