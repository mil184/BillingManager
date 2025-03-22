namespace Api.Dto
{
    public record BillDto
    {
        public DateTime DateTime { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
