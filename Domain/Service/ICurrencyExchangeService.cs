using Domain.Enum;

namespace Domain.Service
{
    public interface ICurrencyExchangeService
    {
        Task<double?> ConvertCurrency(Currency baseCurrency, Currency targetCurrency, double amount);
    }
}
