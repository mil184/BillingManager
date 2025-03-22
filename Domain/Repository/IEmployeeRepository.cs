using Domain.Model;

namespace Domain.Repository
{
    public interface IEmployeeRepository
    {
        Employee Create(Employee employee);
        void Delete(Employee employee);
        IEnumerable<Employee> GetAll();
        Employee? GetById(Guid id);
        void Update(Employee employee);
    }
}
