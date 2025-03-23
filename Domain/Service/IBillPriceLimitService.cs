using Domain.Model;

namespace Domain.Service
{
    public interface IBillPriceLimitService
    {
        BillPriceLimit Create(BillPriceLimit billPriceLimit);
        void Delete(Guid id);
        IEnumerable<BillPriceLimit> GetAll();
        BillPriceLimit? GetById(Guid id);
        BillPriceLimit GetNewest();
        void Update(BillPriceLimit billPriceLimit);
    }
}
