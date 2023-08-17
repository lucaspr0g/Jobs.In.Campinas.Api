using Domain.Interfaces.Services;
using MediatR;
using System.Security.Claims;

namespace Domain.Commands.Account.Refresh
{
    public sealed class RefreshCommandHandler : IRequestHandler<RefreshRequest, TokenResponse>
	{
		private readonly IAccountService _accountService;
        public RefreshCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;	
        }

        public async Task<TokenResponse> Handle(RefreshRequest request, CancellationToken cancellationToken)
		{
			if (!request.IsValid())
				throw new ArgumentException("Dados inválidos.");

			var principal = _accountService.GetPrincipalFromExpiredToken(request.AccessToken!);
			
			var email = principal.Claims
				.First(s => s.Type == ClaimTypes.Email)
				.Value;

			var (accessToken, refreshToken) = await _accountService.ValidateAndUpdateRefreshToken(email, request.RefreshToken!);

			return new TokenResponse { AccessToken = accessToken, RefreshToken = refreshToken };
		}
	}
}