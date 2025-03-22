namespace Domain.Model
{
    public class PaymentCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Pan { get; set; }
        public required string Cvv { get; set; }
    }
}
