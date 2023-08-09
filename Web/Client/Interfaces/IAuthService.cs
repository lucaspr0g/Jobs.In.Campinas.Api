using Web.Client.Entities.Account;

namespace Web.Client.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> Register(RegisterModel registerModel);
        Task<LoginResponse> Login(LoginModel loginModel);
        Task Logout();
    }
}
