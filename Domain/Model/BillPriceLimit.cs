namespace Domain.Model
{
    public class BillPriceLimit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double BillUpperLimit { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
