using Domain.Model;

namespace Domain.Repository
{
    public interface IBillItemRepository
    {
        BillItem Create(BillItem billItem);
        void Delete(BillItem billItem);
        IEnumerable<BillItem> GetAll();
        BillItem? GetById(Guid id);
        void Update(BillItem billItem);
    }
}
