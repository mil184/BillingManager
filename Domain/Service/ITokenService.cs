using Domain.Model;

namespace Domain.Service
{
    public interface ITokenService
    {
        string GenerateToken(Employee employee);
    }
}
