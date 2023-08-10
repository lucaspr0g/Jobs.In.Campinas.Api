using Domain.Commands.Account.Create;
using Domain.Commands.Account.Login;
using Domain.Entities;
using System.Security.Claims;

namespace Domain.Interfaces.Services
{
    public interface IAccountService
    {
		Task<(UserDto, string)> AuthenticateAsync(LoginRequestDto request);
        string GenerateToken(UserDto user);
        Task CreateAsync(AccountCreateRequest request);
        string GetAuthenticatedUserId();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<(string, string)> ValidateAndUpdateRefreshToken(string email, string refreshToken);
	}
}