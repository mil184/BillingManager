using Domain.Enum;

namespace Domain.Model
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public EmployeeRole Role { get; set; }

        public List<Bill>? Bills { get; set; } = new();
    }
}
