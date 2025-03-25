using Api.Dto;
using Api.Helper;
using Domain.Model;
using Domain.Service;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/bill-items")]
    [ApiController]
    public class BillItemController : ControllerBase
    {
        private readonly IBillItemService _billItemService;
        private readonly IBillService _billService;

        public BillItemController(IBillItemService billItemService, IBillService billService)
        {
            _billItemService = billItemService;
            _billService = billService;
        }

        [HttpGet]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult<IEnumerable<BillItem>> GetAll()
        {
            return Ok(_billItemService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult<BillItem> GetById(Guid id)
        {
            var billItem = _billItemService.GetById(id);

            if (billItem == null)
            {
                return NotFound();
            }

            return Ok(billItem);
        }

        [HttpPost]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult<BillItem> Create(BillItemDto billItemDto)
        {
            Guid billId = GuidHelper.GetGuidFromString(billItemDto.BillId);
            Guid productId = GuidHelper.GetGuidFromString(billItemDto.ProductId);

            BillItem billItem = new BillItem()
            {
                Amount = billItemDto.Amount,
                BillId = billId,
                ProductId = productId
            };

            try
            {
                var createdBillItem = _billItemService.Create(billItem);
                return Ok(createdBillItem);
            }
            catch (BillAmountExceedsLimitException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult Update(Guid id, BillItemDto billItemDto)
        {
            var billItem = _billItemService.GetById(id);

            if (billItem == null)
            {
                return NotFound();
            }

            _billItemService.Update(billItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult Delete(Guid id)
        {
            var billItem = _billItemService.GetById(id);

            if (billItem == null)
            {
                return NotFound();
            }

            _billItemService.Delete(id);
            return Ok();
        }
    }
}