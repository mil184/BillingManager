using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;

        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public void ProcessBillPayment(Bill bill)
        {
            bill.isPayed = true;
            Update(bill);
        }

        public void UpdateTotalPrice(Bill bill, double totalPrice)
        {
            bill.TotalPrice = totalPrice;
            Update(bill);
        }

        public Bill Create(Bill bill) => _billRepository.Create(bill);

        public void Delete(Guid id)
        {
            var bill = _billRepository.GetById(id);
            if (bill != null)
                _billRepository.Delete(bill);
        }

        public IEnumerable<Bill> GetAll() => _billRepository.GetAll();

        public Bill? GetById(Guid id) => _billRepository.GetById(id);

        public void Update(Bill bill) => _billRepository.Update(bill);
    }
}
