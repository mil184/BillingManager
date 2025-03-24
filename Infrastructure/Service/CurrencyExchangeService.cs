using Domain.Enum;
using Domain.Service;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.Service
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly string _apiHost;
        private readonly string _baseUrl;

        public CurrencyExchangeService(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _apiKey = configuration["CurrencyExchange:ApiKey"] ?? throw new ArgumentNullException("API Key is missing in appsettings.json");
            _apiHost = configuration["CurrencyExchange:ApiHost"] ?? throw new ArgumentNullException("API Host is missing in appsettings.json");
            _baseUrl = configuration["CurrencyExchange:BaseUrl"] ?? throw new ArgumentNullException("Base URL is missing in appsettings.json");
        }

        public async Task<string> FetchData(Currency baseCurrency, Currency targetCurrency, double amount)
        {
            string body = string.Empty;
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_baseUrl + $"from={baseCurrency.ToString()}&to={targetCurrency.ToString()}&amount={amount}"),
                Headers =
                {
                    { "x-rapidapi-key", _apiKey },
                    { "x-rapidapi-host", _apiHost },
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
                    return convertedAmount;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
            }

            return null;
        }
    }
}
