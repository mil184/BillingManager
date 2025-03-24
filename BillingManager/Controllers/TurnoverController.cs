using Api.Helper;
using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/turnover")]
    [ApiController]
    public class TurnoverController : ControllerBase
    {
        private readonly IBillPriceLimitService _billPriceLimitService;

        public TurnoverController(IBillPriceLimitService billPriceLimitService)
        {
            _billPriceLimitService = billPriceLimitService;
        }

        [HttpPost]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult<BillPriceLimit> Create(double limit)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var employeeId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            BillPriceLimit billPriceLimit = new BillPriceLimit()
            {
                BillUpperLimit = limit,
                DateAdded = DateTime.UtcNow,
                EmployeeId = GuidHelper.GetGuidFromString(employeeId)
            };

            var createdBillPriceLimit = _billPriceLimitService.Create(billPriceLimit);
            //return CreatedAtAction(nameof(GetNewest), createdBillPriceLimit);
            return Ok(createdBillPriceLimit);
        }

        [HttpGet]
        public ActionResult<BillPriceLimit> GetNewest()
        {
            var bill = _billPriceLimitService.GetNewest();
            return bill != null ? Ok(bill) : NotFound();
        }
    }
}
