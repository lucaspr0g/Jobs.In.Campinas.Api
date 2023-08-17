using MediatR;

namespace Domain.Commands.Account.Refresh
{
    public sealed class RefreshRequest : IRequest<TokenResponse>
	{
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(AccessToken) && !string.IsNullOrWhiteSpace(RefreshToken);
    }
}