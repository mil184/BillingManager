using System.Reflection.Metadata.Ecma335;

namespace Domain.Model
{
    public class PaymentCard
    {
        public Guid Id { get; set; }
        public required string Pan { get; set; }
        public required string Cvv { get; set; }
    }
}
