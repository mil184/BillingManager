using Api.Dto;
using Api.Helper;
using Api.Validation;
using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IPaymentService _paymentService;
        private readonly PaymentCardValidator _paymentCardValidator;

        public BillController(IBillService billService, IPaymentService paymentService, PaymentCardValidator paymentCardValidator)
        {
            _billService = billService;
            _paymentService = paymentService;
            _paymentCardValidator = paymentCardValidator;
        }

        [HttpPut]
        [Route("/card-payment")]
        public IActionResult PayWithCard(PaymentDto payment)
        {
            var validationResult = _paymentCardValidator.Validate(payment);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            if (!_paymentService.CheckPaymentCardInformation(payment.Pan, payment.Cvv))
            {
                return StatusCode(401, "Invalid card details.");
            }

            Bill bill = _billService.GetById(GuidHelper.GetGuidFromString(payment.BillId));
            _billService.ProcessBillPayment(bill);
            return Ok();
        }

        [HttpPost]
        public ActionResult<Bill> Create(BillDto billDto)
        {
            if (billDto == null)
            {
                return BadRequest();
            }

            Bill bill = new Bill()
            {
                DateTime = billDto.DateTime,
                EmployeeId = GuidHelper.GetGuidFromString(billDto.EmployeeId),
                BillNumber = billDto.BillNumber
            };


            var createdBill = _billService.Create(bill);
            return CreatedAtAction(nameof(GetById), new { id = createdBill.Id }, createdBill);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Bill>> GetAll()
        {
            return Ok(_billService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Bill> GetById(Guid id)
        {
            var bill = _billService.GetById(id);
            return bill != null ? Ok(bill) : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Bill bill)
        {
            if (id != bill.Id) return BadRequest();
            _billService.Update(bill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _billService.Delete(id);
            return NoContent();
        }
    }

}
