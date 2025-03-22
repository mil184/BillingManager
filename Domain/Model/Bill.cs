namespace Domain.Model
{
    public class Bill
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime DateTime { get; set; }
        public double TotalAmount { get; set; } = 0;
        public bool isPayed { get; set; } = false;

        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public IEnumerable<BillItem>? BillItems { get; set; } = new List<BillItem>();

    }
}
