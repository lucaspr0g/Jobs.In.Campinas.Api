using Web.Client.Entities.Account;

namespace Web.Client.Interfaces
{
    public interface IAuthService
    {
		Task<(bool, string)> Register(RegisterModel registerModel);
		Task<bool> Login(LoginModel loginModel);
        Task Logout();
        Task<string> RefreshToken();
		Task<string?> TryRefreshToken();
		Task<(bool, string?)> ConfirmAccount(string token, string email);
	}
}