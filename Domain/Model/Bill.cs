namespace Domain.Model
{
    public class Bill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateTime { get; set; }
        public double TotalPrice { get; set; } = 0;
        public required string BillNumber { get; set; }
        public bool IsPayed { get; set; } = false;

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public List<BillItem> BillItems { get; set; } = new List<BillItem>();

    }
}
