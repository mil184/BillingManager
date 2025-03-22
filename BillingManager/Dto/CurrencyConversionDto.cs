namespace Api.Dto
{
    public record CurrencyConversionDto
    {
        public required string baseCurrency { get; set; }
        public required string targetCurrency { get; set; }
        public required double amount { get; set; }
    }
}
