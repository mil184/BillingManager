using Domain.Model;

namespace Domain.Repository
{
    public interface IBillPriceLimitRepository
    {
        BillPriceLimit Create(BillPriceLimit billPriceLimit);
        void Delete(BillPriceLimit billPriceLimit);
        IEnumerable<BillPriceLimit> GetAll();
        BillPriceLimit? GetById(Guid id);
        void Update(BillPriceLimit billPriceLimit);
    }
}
