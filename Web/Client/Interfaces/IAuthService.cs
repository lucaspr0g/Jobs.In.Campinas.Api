using Web.Client.Entities.Account;

namespace Web.Client.Interfaces
{
    public interface IAuthService
    {
		Task<(bool, string)> Register(RegisterModel registerModel);
		Task<bool> Login(LoginModel loginModel);
        Task Logout();
    }
}