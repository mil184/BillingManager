namespace Domain.Model
{
    public class BillItem
    {
        public Guid Id { get; set; } = new Guid();
        public int Amount { get; set; }
        public double Price { get; set; }

        public Guid BillId { get; set; }
        //public Bill? Bill { get; set; }

        public Guid ProductId { get; set; }
        //public Product? Product { get; set; }
    }
}
