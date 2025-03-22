using Domain.Model;
using Domain.Repository;
using Infrastructure.DataContext;

namespace Infrastructure.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly AppDbContext _context;

        public BillRepository(AppDbContext context)
        {
            _context = context;
        }

        public Bill Create(Bill bill)
        {
            _context.Bills.Add(bill);
            _context.SaveChanges();
            return bill;
        }

        public void Delete(Bill bill)
        {
            _context.Bills.Remove(bill);
            _context.SaveChanges();
        }

        public IEnumerable<Bill> GetAll()
        {
            return _context.Bills.ToList();
        }

        public Bill? GetById(Guid id)
        {
            return _context.Bills.FirstOrDefault(b => b.Id == id);
        }

        public void Update(Bill bill)
        {
            _context.Bills.Update(bill);
            _context.SaveChanges();
        }
    }
}
