using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/aaa")]
    [ApiController]
    public class AController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public AController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("get-employees")]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            return Ok(employeeService.GetAll());
        }
    }
}
