using Domain.Model;

namespace Domain.Repository
{
    public interface IBillRepository
    {
        Bill Create(Bill bill);
        void Delete(Bill bill);
        IEnumerable<Bill> GetAll();
        Bill? GetById(Guid id);
        void Update(Bill bill);
    }
}
