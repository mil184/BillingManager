using Domain.Enum;

namespace Domain.Model
{
    public class Employee
    {
        public Guid Id { get; set; } = new Guid();
        public string? Name { get; set; }
        public EmployeeRole Role { get; set; }

        //public IEnumerable<Bill>? Bills { get; set; } = new List<Bill>();
    }
}
