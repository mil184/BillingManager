using Api.Controllers;
using Api.Dto;
using Api.Validation;
using Domain.Model;
using Domain.Service;
using FakeItEasy;
using Infrastructure.Service;
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

        private readonly ITokenService _tokenService;

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

            _tokenService = new TokenService();
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


    }
}