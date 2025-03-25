using Domain.Model;
using Domain.Service;
using System.Security.Authentication;

namespace Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeService _employeeService;
        public AuthService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public Employee LogIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new AuthenticationException("Email and password must be provided.");
            }

            Employee employee = _employeeService.GetByEmail(email);

            if (employee == null)
            {
                throw new AuthenticationException("Email doesn't exist.");
            }

            if (!CheckPassword(employee, password))
            {
                throw new AuthenticationException("Wrong password.");
            }

            return employee;

        }

        private bool CheckPassword(Employee employee, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, employee.Password);
        }
    }
}
