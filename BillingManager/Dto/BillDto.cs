namespace Api.Dto
{
    public record BillDto
    {
        public DateTime DateTime { get; set; }
        public required string BillNumber { get; set; }

    }
}
