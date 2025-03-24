using Api.Controllers;
using Api.Dto;
using Api.Validation;
using Domain.Model;
using Domain.Service;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            BillDto billDto = new BillDto { DateTime = DateTime.Now, BillNumber = "12345" };
            Bill bill = new Bill { Id = Guid.NewGuid(), DateTime = billDto.DateTime, BillNumber = billDto.BillNumber };

            //A.CallTo(() => _billValidator.Validate(A<Bill>._)).Returns(new ValidationResult());
            var validationResult = _billValidator.Validate(bill);
            A.CallTo(() => _billService.Create(A<Bill>._)).Returns(bill);

            var result = _controller.Create(billDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(bill.Id, ((Bill)createdAtActionResult.Value).Id);
        }

        //public PaymentTest()
        //{
        //    _billService = new Mock<IBillService>();
        //    _paymentCardValidator = new PaymentCardValidator();
        //    _paymentService = new Mock<IPaymentService>();

        //    _billController = new BillController(_billService.Object, _paymentService.Object, _paymentCardValidator);
        //}

        //[Fact]
        //public void Test_ValidPayment_ShouldProcessPayment()
        //{
        //    // Arrange
        //    PaymentDto validPayment = new PaymentDto()
        //    {
        //        BillId = "656EE4CB-AB2A-4596-8DF4-6299DF7CFB66",
        //        Pan = "5500000000000004",
        //        Cvv = "456"
        //    };

        //    Bill mockBill = new Bill() { BillNumber = "1010" };

        //    _billService.Setup(s => s.GetById(It.IsAny<Guid>())).Returns(mockBill);
        //    _billService.Setup(s => s.ProcessBillPayment(It.IsAny<Bill>()));

        //    var validationResult = _paymentCardValidator.Validate(validPayment);

        //    _paymentService
        //        .Setup(s => s.CheckPaymentCardInformation(validPayment.Pan, validPayment.Cvv))
        //        .Returns(true);

        //    // Act
        //    var result = _controller.PayWithCard(validPayment);

        //    // Assert
        //    Assert.IsType<OkResult>(result);
        //}

        // 

        //[Fact]
        //public void Test_InvalidPayment_NoPan()
        //{
        //    // Arrange
        //    PaymentDto invalidPayment = new PaymentDto()
        //    {
        //        BillId = "656EE4CB-AB2A-4596-8DF4-6299DF7CFB66",
        //        Pan = "",
        //        Cvv = "456"
        //    };

        //    Bill mockBill = new Bill();

        //    _billService.Setup(s => s.GetById(It.IsAny<Guid>())).Returns(mockBill);
        //    _billService.Setup(s => s.ProcessBillPayment(It.IsAny<Bill>()));

        //    var validationResult = _paymentCardValidator.Validate(invalidPayment);

        //    _paymentService
        //        .Setup(s => s.CheckPaymentCardInformation(invalidPayment.Pan, invalidPayment.Cvv))
        //        .Returns(true);

        //    // Act
        //    var result = _billController.PayWithCard(invalidPayment);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    var errorMessages = badRequestResult.Value as IEnumerable<string>;

        //    // Assert that the error message contains "Pan" being required
        //    Assert.Contains("Pan is required", errorMessages);
        //}
    }
}