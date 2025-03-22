using Api.Dto;
using Api.Helper;
using Domain.Enum;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/currency-exchange")]
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public CurrencyExchangeController(ICurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> GetExchange([FromBody] CurrencyConversionDto currencyConversionDto)
        {
            Currency baseCurrency = CurrencyHelper.GetCurrencyFromString(currencyConversionDto.baseCurrency);
            Currency targetCurrency = CurrencyHelper.GetCurrencyFromString(currencyConversionDto.targetCurrency);

            var amount = await _currencyExchangeService.ConvertCurrency(baseCurrency, targetCurrency, currencyConversionDto.amount);
            return Ok(amount);
        }
    }
}
