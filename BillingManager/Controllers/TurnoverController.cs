using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/turnover")]
    [ApiController]
    public class TurnoverController : ControllerBase
    {
        private readonly IConfigurationRoot _configRoot;

        public TurnoverController(IConfiguration configRoot)
        {
            _configRoot = (IConfigurationRoot)configRoot;
        }

        [HttpPut]
        public IActionResult UpdateUpperLimit(double newUpperLimit)
        {

            return Ok();
        }
    }
}
