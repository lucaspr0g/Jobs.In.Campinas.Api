using Domain.Commands.Account.Create;
using Domain.Commands.Account.Login;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Task<UserDto> AuthenticateAsync(LoginRequestDto request);
        string GenerateToken(UserDto user);
        Task<AccountCreateResponse> CreateAsync(AccountCreateRequest request);
        Task LogoffAsync();
        Task<UserDto> GetAuthenticatedUser(string userId);
    }
}