using Domain.Interfaces.Services;
using Mapster;
using MediatR;

namespace Domain.Commands.Account.Login
{
    public sealed class LoginUserCommandHandler : IRequestHandler<LoginRequest, TokenResponse>
    {
        private readonly IAccountService _accountService;

        public LoginUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<TokenResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("Unauthorized");

            var dto = request.Adapt<LoginRequestDto>();

            var (user, refreshToken) = await _accountService.AuthenticateAsync(dto);
            var token = _accountService.GenerateToken(user);

            return new TokenResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }
    }
}