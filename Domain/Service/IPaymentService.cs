namespace Domain.Service
{
    public interface IPaymentService
    {
        bool CheckPaymentCardInformation(string pan, string cvv);
    }
}
