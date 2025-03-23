using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITokenService _tokenService;

        public AuthController(IEmployeeService employeeService, ITokenService tokenService)
        {
            _employeeService = employeeService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(string name)
        {
            Employee employee = _employeeService.GetByName(name);

            if (employee == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(employee);
            return Ok(new { Token = token });
        }
    }
}
