using Domain.Model;

namespace Domain.Service
{
    public interface IAuthService
    {
        Employee LogIn(string email, string password);
    }
}
