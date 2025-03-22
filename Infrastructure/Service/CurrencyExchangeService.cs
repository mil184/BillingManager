using Domain.Enum;
using Domain.Service;
using System.Text.Json;

namespace Infrastructure.Service
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly HttpClient _client = new HttpClient();
        private const string API_KEY = "a431120ba4mshf3948c89893233cp1c5610jsncd5b2ee17404";
        private const string API_HOST = "currency-conversion-and-exchange-rates.p.rapidapi.com";
        private const string BASE_URL = "https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?";

        public async Task<string> FetchData(Currency baseCurrency, Currency targetCurrency, double amount)
        {
            string body = string.Empty;
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BASE_URL + $"from={baseCurrency.ToString()}&to={targetCurrency.ToString()}&amount={amount}"),
                //RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=USD&to=EUR&amount=750"),
                Headers =
                {
                    { "x-rapidapi-key", API_KEY },
                    { "x-rapidapi-host", API_HOST },
                }
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }

            return body;
        }

        public async Task<double?> ConvertCurrency(Currency baseCurrency, Currency targetCurrency, double amount)
        {
            string body = await FetchData(baseCurrency, targetCurrency, amount);

            try
            {
                using JsonDocument doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("result", out JsonElement resultElement) && resultElement.TryGetDouble(out double convertedAmount))
                {
                    return convertedAmount; // Returning a proper numeric value
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
            }

            return null; // Returning null instead of empty string
        }
    }
}
