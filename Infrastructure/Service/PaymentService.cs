using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public bool CheckPaymentCardInformation(string pan, string cvv)
        {
            PaymentCard paymentCard = _paymentRepository.GetByPan(pan);
            return paymentCard.Cvv == cvv;
        }


    }
}
