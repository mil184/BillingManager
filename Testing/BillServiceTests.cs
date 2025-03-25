using Domain.Model;
using Domain.Repository;
using Domain.Service;
using FakeItEasy;
using Infrastructure.Exceptions;
using Infrastructure.Service;

namespace Testing
{
    public class BillServiceTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillPriceLimitService _billPriceLimitService;
        private readonly IBillService _service;

        public BillServiceTests()
        {
            _billRepository = A.Fake<IBillRepository>();
            _billPriceLimitService = A.Fake<IBillPriceLimitService>();
            _service = new BillService(_billRepository, _billPriceLimitService);
        }

        [Fact]
        public void Create_ShouldCallRepositoryCreateMethod()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), BillNumber = "12345" };
            A.CallTo(() => _billRepository.Create(bill)).Returns(bill);

            // Act
            var result = _service.Create(bill);

            // Assert
            A.CallTo(() => _billRepository.Create(bill)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void BillService_ProcessBillPayment_ShouldUpdateBillAsPayed()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };

            // Act
            _service.ProcessBillPayment(bill);

            // Assert
            A.CallTo(() => _billRepository.Update(bill)).MustHaveHappenedOnceExactly();
            Assert.True(bill.IsPayed);
        }

        [Fact]
        public void BillService_UpdateTotalPrice_ShouldThrowException_WhenPriceExceedsLimit()
        {
            // Arrange
            Bill bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };
            BillPriceLimit billPriceLimit = new BillPriceLimit { BillUpperLimit = 100 };
            double price = 200;

            A.CallTo(() => _billPriceLimitService.GetNewest()).Returns(billPriceLimit);

            // Act
            var exception = Assert.Throws<BillAmountExceedsLimitException>(() => _service.UpdateTotalPrice(bill, price));

            // Assert
            Assert.Equal("Bill total exceeds the upper limit.", exception.Message);
        }

        [Fact]
        public void BillService_Delete_ShouldDelete()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };
            A.CallTo(() => _billRepository.GetById(bill.Id)).Returns(bill);

            // Act
            _service.Delete(bill.Id);

            // Assert
            A.CallTo(() => _billRepository.Delete(bill)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void BillService_Delete_ShouldThrowException()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };
            A.CallTo(() => _billRepository.GetById(bill.Id)).Returns(null);

            // Act
            var exception = Assert.Throws<BillIsNullException>(() => _service.Delete(bill.Id));

            // Assert
            Assert.Equal("Bill is null.", exception.Message);
        }

        [Fact]
        public void BillService_GetById_ShouldDelete()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };
            A.CallTo(() => _billRepository.GetById(bill.Id)).Returns(bill);

            // Act
            _service.GetById(bill.Id);

            // Assert
            A.CallTo(() => _billRepository.GetById(bill.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void BillService_GetById_ShouldThrowException()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };
            A.CallTo(() => _billRepository.GetById(bill.Id)).Returns(null);

            // Act
            var exception = Assert.Throws<BillIsNullException>(() => _service.GetById(bill.Id));

            // Assert
            Assert.Equal("Bill is null.", exception.Message);
        }

        [Fact]
        public void BillService_Update_ShouldDelete()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };
            A.CallTo(() => _billRepository.GetById(bill.Id)).Returns(bill);

            // Act
            _service.Update(bill);

            // Assert
            A.CallTo(() => _billRepository.Update(bill)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void BillService_Update_ShouldThrowException()
        {
            // Arrange
            var bill = new Bill { Id = Guid.NewGuid(), IsPayed = false, BillNumber = "12345" };
            A.CallTo(() => _billRepository.GetById(bill.Id)).Returns(null);

            // Act
            var exception = Assert.Throws<BillIsNullException>(() => _service.Update(bill));

            // Assert
            Assert.Equal("Bill is null.", exception.Message);
        }

    }
}
