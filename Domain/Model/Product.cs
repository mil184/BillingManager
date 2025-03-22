namespace Domain.Model
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public double Price { get; set; }

        public List<BillItem> BillItems { get; set; } = new();
    }
}
