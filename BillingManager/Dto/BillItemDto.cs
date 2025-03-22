namespace Api.Dto
{
    public record BillItemDto
    {
        public required int Amount { get; set; }
        public required string BillId { get; set; }
        public required string ProductId { get; set; }
    }
}
