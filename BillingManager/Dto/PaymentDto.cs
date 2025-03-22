namespace Api.Dto
{
    public record PaymentDto
    {
        public required string BillId { get; set; }
        public required string Pan { get; set; }
        public required string Cvv { get; set; }
    }
}
