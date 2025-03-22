using Domain.Model;
using Domain.Repository;
using Infrastructure.DataContext;

namespace Infrastructure.Repository
{
    public class BillItemRepository : IBillItemRepository
    {
        private readonly AppDbContext _context;

        public BillItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public BillItem Create(BillItem billItem)
        {
            _context.BillItems.Add(billItem);
            _context.SaveChanges();
            return billItem;
        }

        public void Delete(BillItem billItem)
        {
            _context.BillItems.Remove(billItem);
            _context.SaveChanges();
        }

        public IEnumerable<BillItem> GetAll()
        {
            return _context.BillItems.ToList();
        }

        public BillItem? GetById(Guid id)
        {
            return _context.BillItems.FirstOrDefault(bi => bi.Id == id);
        }

        public void Update(BillItem billItem)
        {
            _context.BillItems.Update(billItem);
            _context.SaveChanges();
        }
    }
}
