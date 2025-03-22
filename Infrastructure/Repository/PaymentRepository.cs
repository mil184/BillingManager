using Domain.Model;
using Domain.Repository;
using Infrastructure.DataContext;

namespace Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PaymentCard> GetAll()
        {
            return _context.PaymentCards;
        }

        public PaymentCard GetByPan(string pan)
        {
            return GetAll().FirstOrDefault(x => x.Pan == pan);
        }
    }
}
