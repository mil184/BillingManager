using Api.Controllers;
using Api.Dto;
using Api.Validation;
using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testing
{

    public class PaymentTest
    {
        private readonly BillController _billController;
        private readonly PaymentCardValidator _paymentCardValidator;
        private readonly Mock<IBillService> _billService;
        private readonly Mock<IPaymentService> _paymentService;

        public PaymentTest()
        {
            _billService = new Mock<IBillService>();
            _paymentCardValidator = new PaymentCardValidator();
            _paymentService = new Mock<IPaymentService>();

            _billController = new BillController(_billService.Object, _paymentService.Object, _paymentCardValidator);
        }

        [Fact]
        public void Test_ValidPayment_ShouldProcessPayment()
        {
            // Arrange
            PaymentDto validPayment = new PaymentDto()
            {
                BillId = "656EE4CB-AB2A-4596-8DF4-6299DF7CFB66",
                Pan = "5500000000000004",
                Cvv = "456"
            };

            Bill mockBill = new Bill();

            _billService.Setup(s => s.GetById(It.IsAny<Guid>())).Returns(mockBill);
            _billService.Setup(s => s.ProcessBillPayment(It.IsAny<Bill>()));

            var validationResult = _paymentCardValidator.Validate(validPayment);

            _paymentService
                .Setup(s => s.CheckPaymentCardInformation(validPayment.Pan, validPayment.Cvv))
                .Returns(true);

            // Act
            var result = _billController.PayWithCard(validPayment);

            // Assert
            Assert.IsType<OkResult>(result);
        }

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