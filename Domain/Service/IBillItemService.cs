using Domain.Model;

namespace Domain.Service
{
    public interface IBillItemService
    {
        BillItem Create(BillItem billItem);
        void Delete(Guid id);
        IEnumerable<BillItem> GetAll();
        BillItem? GetById(Guid id);
        void Update(BillItem billItem);
    }
}
