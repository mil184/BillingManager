namespace Domain.Model
{
    public class BillItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Amount { get; set; }
        public double Price { get; set; }

        public Guid BillId { get; set; }
        public Bill Bill { get; set; } = null!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
