using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Create(Employee employee) => _employeeRepository.Create(employee);

        public void Delete(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee != null)
                _employeeRepository.Delete(employee);
        }

        public IEnumerable<Employee> GetAll() => _employeeRepository.GetAll();

        public Employee? GetById(Guid id) => _employeeRepository.GetById(id);

        public void Update(Employee employee) => _employeeRepository.Update(employee);
    }
}
