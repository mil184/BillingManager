using Domain.Model;

namespace Domain.Service
{
    public interface IBillService
    {
        void UpdateTotalPrice(Bill bill, double totalPrice);
        void ProcessBillPayment(Bill bill);
        Bill Create(Bill bill);
        void Delete(Guid id);
        IEnumerable<Bill> GetAll();
        Bill? GetById(Guid id);
        void Update(Bill bill);
    }
}
