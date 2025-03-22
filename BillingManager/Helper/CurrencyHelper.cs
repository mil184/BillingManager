using Domain.Enum;

namespace Api.Helper
{
    public static class CurrencyHelper
    {
        public static Currency GetCurrencyFromString(string currencyString)
        {
            if (Enum.TryParse(currencyString, out Currency currency))
            {
                return currency;
            }
            else
            {
                throw new ArgumentException("Invalid currency string");
            }

        }
    }
}
