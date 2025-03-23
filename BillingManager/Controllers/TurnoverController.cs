using Api.Helper;
using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<BillPriceLimit> Create(double limit)
        {
            BillPriceLimit billPriceLimit = new BillPriceLimit()
            {
                BillUpperLimit = limit,
                DateAdded = DateTime.UtcNow,
                EmployeeId = GuidHelper.GetGuidFromString("710a0a89-e30f-455c-99ac-6c7d9cbdec57")
            };

            var createdBillPriceLimit = _billPriceLimitService.Create(billPriceLimit);
            return CreatedAtAction(nameof(GetNewest), createdBillPriceLimit);
        }

        [HttpGet]
        public ActionResult<BillPriceLimit> GetNewest()
        {
            var bill = _billPriceLimitService.GetNewest();
            return bill != null ? Ok(bill) : NotFound();
        }
    }
}
