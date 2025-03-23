namespace Api.Controllers
{
    using Api.Dto;
    using Api.Helper;
    using Domain.Model;
    using Domain.Service;
    using Infrastructure.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    namespace API.Controllers
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
            public ActionResult<IEnumerable<BillItem>> GetAll()
            {
                return Ok(_billItemService.GetAll());
            }

            [HttpGet("{id}")]
            public ActionResult<BillItem> GetById(Guid id)
            {
                var billItem = _billItemService.GetById(id);
                return billItem != null ? Ok(billItem) : NotFound();
            }

            [HttpPost]
            public ActionResult<BillItem> Create(BillItemDto billItemDto)
            {
                // validate!!!
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
            public IActionResult Update(Guid id, BillItem billItem)
            {
                if (id != billItem.Id) return BadRequest();
                _billItemService.Update(billItem);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(Guid id)
            {
                _billItemService.Delete(id);
                return NoContent();
            }
        }
    }

}
