using Domain.Model;

namespace Domain.Service
{
    public interface IEmployeeService
    {
        Employee GetByName(string name);
        Employee Create(Employee employee);
        void Delete(Guid id);
        IEnumerable<Employee> GetAll();
        Employee? GetById(Guid id);
        void Update(Employee employee);
    }
}
