using Domain.Enum;

namespace Domain.Model
{
    public class Employee
    {
        public Guid Id { get; set; } = new Guid();
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public EmployeeRole Role { get; set; }

        //public IEnumerable<Bill>? Bills { get; set; } = new List<Bill>();
    }
}
