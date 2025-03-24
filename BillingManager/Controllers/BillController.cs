using Api.Dto;
using Api.Helper;
using Api.Validation;
using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IPaymentService _paymentService;

        private readonly PaymentCardValidator _paymentCardValidator;
        private readonly BillValidator _billValidator;

        public BillController(IBillService billService, IPaymentService paymentService, PaymentCardValidator paymentCardValidator, BillValidator billValidator)
        {
            _billService = billService;
            _paymentService = paymentService;
            _paymentCardValidator = paymentCardValidator;
            _billValidator = billValidator;
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
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult<Bill> Create(BillDto billDto)
        {
            if (billDto == null)
            {
                return BadRequest();
            }

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var employeeId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            Bill bill = new Bill()
            {
                DateTime = billDto.DateTime,
                EmployeeId = GuidHelper.GetGuidFromString(employeeId),
                BillNumber = billDto.BillNumber
            };

            var validationResult = _billValidator.Validate(bill);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var createdBill = _billService.Create(bill);
            //return CreatedAtAction(nameof(GetById), new { id = createdBill.Id }, createdBill);
            return Ok(createdBill);
        }

        [HttpGet]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult<IEnumerable<Bill>> GetAll()
        {
            return Ok(_billService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "CashRegisterOfficer")]
        public ActionResult<Bill> GetById(Guid id)
        {
            Bill? bill = _billService.GetById(id);

            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "CashRegisterOfficer")]
        public IActionResult Update(Guid id, BillDto billDto)
        {
            Bill? bill = _billService.GetById(id);

            if (bill == null)
            {
                return NotFound();
            }

            _billService.Update(bill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "CashRegisterOfficer")]
        public IActionResult Delete(Guid id)
        {
            Bill? bill = _billService.GetById(id);

            if (bill == null)
            {
                return NotFound();
            }

            _billService.Delete(id);
            return NoContent();
        }
    }

}
