using Domain.Interfaces.Services;
using Mapster;
using MediatR;

namespace Domain.Commands.Account.Login
{
    public sealed class LoginUserCommandHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IAccountService _accountService;

        public LoginUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("dados invalidos");

            var dto = request.Adapt<LoginRequestDto>();

            var user = await _accountService.AuthenticateAsync(dto);
            var token = _accountService.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                User = new User(user.Id, user.Email, user.Name)
            };
        }
    }
}