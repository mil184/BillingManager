using Api.Controllers;
using Api.Dto;
using Api.Validation;
using Domain.Model;
using Domain.Service;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Testing
{
    public class BillControllerTests
    {
        private readonly IBillService _billService;
        private readonly IPaymentService _paymentService;

        private readonly PaymentCardValidator _paymentCardValidator;
        private readonly BillValidator _billValidator;

        private readonly BillController _controller;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BillControllerTests()
        {
            _billService = A.Fake<IBillService>();
            _paymentService = A.Fake<IPaymentService>();
            _paymentCardValidator = new PaymentCardValidator();
            _billValidator = new BillValidator();

            var httpContext = A.Fake<HttpContext>();
            var headers = A.Fake<IHeaderDictionary>();

            // Mock the "Authorization" header
            A.CallTo(() => headers["Authorization"]).Returns("Bearer someTokenHere");
            A.CallTo(() => httpContext.Request.Headers).Returns(headers);

            // Inject the mocked HttpContextAccessor into the controller
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => _httpContextAccessor.HttpContext).Returns(httpContext);

            _controller = new BillController(_billService, _paymentService, _paymentCardValidator, _billValidator);
        }

        [Fact]
        public void BillController_Create_ReturnOk()
        {
            // Arrange
            BillDto billDto = new BillDto { DateTime = DateTime.Now, BillNumber = "260-0056010016113-79" };
            Bill bill = new Bill { Id = Guid.NewGuid(), DateTime = billDto.DateTime, BillNumber = billDto.BillNumber };
            Employee employee = new Employee() { Id = Guid.NewGuid(), Name = "Milica", Role = Domain.Enum.EmployeeRole.CashRegisterOfficer };

            A.CallTo(() => _billService.Create(A<Bill>._)).Returns(bill);

            var fakeJwtToken = new JwtSecurityToken(
                claims: [new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString())]
                 );

            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(fakeJwtToken);

            var fakeHttpContext = A.Fake<HttpContext>();
            var fakeRequest = A.Fake<HttpRequest>();
            var fakeHeaders = new HeaderDictionary { { "Authorization", $"Bearer {tokenString}" } };

            A.CallTo(() => fakeHttpContext.Request).Returns(fakeRequest);
            A.CallTo(() => fakeRequest.Headers).Returns(fakeHeaders);

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            // Act
            var result = _controller.Create(billDto);

            // Assert
            var createdAtActionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBill = Assert.IsType<Bill>(createdAtActionResult.Value);
            Assert.Equal(bill.Id, returnedBill.Id);
        }

        [Fact]
        public void BillController_Create_ReturnBadRequest_WhenBillDtoIsNull()
        {
            // Arrange
            BillDto nullBillDto = null;

            // Act
            var result = _controller.Create(nullBillDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errorMessages = Assert.IsType<List<string>>(badRequestResult.Value);
            Assert.Contains("BillDto cannot be null.", errorMessages);
        }

        [Fact]
        public void BillController_Create_ReturnBadRequest_WhenBillDtoIsInvalid()
        {
            // Arrange
            BillDto billDto = new BillDto { DateTime = DateTime.Now, BillNumber = "12345" };

            // Act
            var result = _controller.Create(billDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errorMessages = Assert.IsType<List<string>>(badRequestResult.Value);
            Assert.Contains("Invalid bill number.", errorMessages);
        }

        [Fact]
        public void BillController_GetAll_ReturnOk()
        {
            // Arrange
            var bills = new List<Bill>();
            A.CallTo(() => _billService.GetAll()).Returns(bills);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBills = Assert.IsType<List<Bill>>(okResult.Value);
        }

        [Fact]
        public void BillController_GetById_ReturnOk()
        {
            // Arrange
            Bill bill = new Bill { Id = Guid.NewGuid(), DateTime = DateTime.Now, BillNumber = "12345" };
            A.CallTo(() => _billService.GetById(bill.Id)).Returns(bill);

            // Act
            var result = _controller.GetById(bill.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBill = Assert.IsType<Bill>(okResult.Value);
        }

        [Fact]
        public void BillController_GetById_ReturnNotFound()
        {
            // Arrange
            Guid billId = Guid.NewGuid();
            A.CallTo(() => _billService.GetById(billId)).Returns(null);

            // Act
            var result = _controller.GetById(billId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void BillController_Update_ReturnOk()
        {
            // Arrange
            BillDto billDto = new BillDto { DateTime = DateTime.Now, BillNumber = "260-0056010016113-79" };
            Bill bill = new Bill { Id = Guid.NewGuid(), DateTime = billDto.DateTime, BillNumber = billDto.BillNumber };
            A.CallTo(() => _billService.GetById(bill.Id)).Returns(bill);

            // Act
            var result = _controller.Update(bill.Id, billDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void BillController_Update_ReturnNotFound()
        {
            // Arrange
            BillDto billDto = new BillDto { DateTime = DateTime.Now, BillNumber = "260-0056010016113-79" };
            Bill bill = new Bill { Id = Guid.NewGuid(), DateTime = billDto.DateTime, BillNumber = billDto.BillNumber };
            A.CallTo(() => _billService.GetById(bill.Id)).Returns(null);

            // Act
            var result = _controller.Update(bill.Id, billDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void BillController_Delete_ReturnOk()
        {
            // Arrange
            BillDto billDto = new BillDto { DateTime = DateTime.Now, BillNumber = "260-0056010016113-79" };
            Bill bill = new Bill { Id = Guid.NewGuid(), DateTime = billDto.DateTime, BillNumber = billDto.BillNumber };
            A.CallTo(() => _billService.GetById(bill.Id)).Returns(bill);

            // Act
            var result = _controller.Delete(bill.Id);

            // Assert
            var okResult = Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void BillController_Delete_ReturnNotFound()
        {
            // Arrange
            BillDto billDto = new BillDto { DateTime = DateTime.Now, BillNumber = "260-0056010016113-79" };
            Bill bill = new Bill { Id = Guid.NewGuid(), DateTime = billDto.DateTime, BillNumber = billDto.BillNumber };
            A.CallTo(() => _billService.GetById(bill.Id)).Returns(null);

            // Act
            var result = _controller.Delete(bill.Id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void BillController_PayWithCard_ReturnOk()
        {
            // Arrange
            Guid billId = Guid.NewGuid();
            PaymentDto paymentDto = new PaymentDto { Pan = "4111111111111111", Cvv = "123", BillId = billId.ToString() };
            Bill bill = new Bill { Id = billId, BillNumber = "12345" };
            var validationResult = _paymentCardValidator.Validate(paymentDto);

            A.CallTo(() => _paymentService.CheckPaymentCardInformation(paymentDto.Pan, paymentDto.Cvv)).Returns(true);
            A.CallTo(() => _billService.GetById(billId)).Returns(bill);

            // Act
            var result = _controller.PayWithCard(paymentDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result.Result);
        }
    }
}