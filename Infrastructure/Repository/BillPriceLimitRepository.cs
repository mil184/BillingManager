using Domain.Model;
using Domain.Repository;
using Infrastructure.DataContext;

namespace Infrastructure.Repository
{
    public class BillPriceLimitRepository : IBillPriceLimitRepository
    {
        private readonly AppDbContext _context;

        public BillPriceLimitRepository(AppDbContext context)
        {
            _context = context;
        }

        public BillPriceLimit Create(BillPriceLimit billPriceLimit)
        {
            _context.BillPriceLimits.Add(billPriceLimit);
            _context.SaveChanges();
            return billPriceLimit;
        }

        public void Delete(BillPriceLimit billPriceLimit)
        {
            _context.BillPriceLimits.Remove(billPriceLimit);
            _context.SaveChanges();
        }

        public IEnumerable<BillPriceLimit> GetAll()
        {
            return _context.BillPriceLimits.ToList();
        }

        public BillPriceLimit? GetById(Guid id)
        {
            return _context.BillPriceLimits.FirstOrDefault(b => b.Id == id);
        }

        public void Update(BillPriceLimit billPriceLimit)
        {
            _context.BillPriceLimits.Update(billPriceLimit);
            _context.SaveChanges();
        }
    }
}
