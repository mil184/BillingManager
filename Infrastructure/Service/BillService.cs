using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Infrastructure.Exceptions;

namespace Infrastructure.Service
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillPriceLimitService _billPriceLimitService;

        public BillService(IBillRepository billRepository, IBillPriceLimitService billPriceLimitService)
        {
            _billRepository = billRepository;
            _billPriceLimitService = billPriceLimitService;
        }

        public void ProcessBillPayment(Bill bill)
        {
            bill.IsPayed = true;
            Update(bill);
        }

        public void UpdateTotalPrice(Bill bill, double price)
        {
            bill.TotalAmount += price;

            if (!IsPriceUnderLimit(price))
            {
                throw new BillAmountExceedsLimitException("Bill total exceeds the upper limit.");
            }

            Update(bill);
        }

        private bool IsPriceUnderLimit(double price)
        {
            BillPriceLimit billPriceLimit = _billPriceLimitService.GetNewest();
            return billPriceLimit.BillUpperLimit > price;
        }

        public Bill Create(Bill bill) => _billRepository.Create(bill);

        public void Delete(Guid id)
        {
            var bill = _billRepository.GetById(id);

            if (bill == null)
            {
                throw new BillIsNullException("Bill is null.");
            }

            _billRepository.Delete(bill);
        }

        public IEnumerable<Bill> GetAll() => _billRepository.GetAll();

        public Bill? GetById(Guid id)
        {
            var bill = _billRepository.GetById(id);

            if (bill == null)
            {
                throw new BillIsNullException("Bill is null.");
            }

            return bill;
        }

        public void Update(Bill bill)
        {
            var oldBill = _billRepository.GetById(bill.Id);

            if (oldBill == null)
            {
                throw new BillIsNullException("Bill is null.");
            }

            _billRepository.Update(bill);
        }
    }
}
