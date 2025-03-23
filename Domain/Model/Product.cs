namespace Domain.Model
{
    public class Product
    {
        public Guid Id { get; set; } = new Guid();
        public required string Name { get; set; }
        public double Price { get; set; }

        //public IEnumerable<BillItem> BillItems { get; set; } = new List<BillItem>();
    }
}
