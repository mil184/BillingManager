using Domain.Model;

namespace Domain.Repository
{
    public interface IPaymentRepository
    {
        IEnumerable<PaymentCard> GetAll();
        PaymentCard GetByPan(string pan);
    }
}
