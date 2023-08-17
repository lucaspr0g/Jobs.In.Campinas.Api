using MediatR;

namespace Domain.Commands.Account.Confirm
{
	public sealed class ConfirmRequest : IRequest<Unit>
	{
        public string? Token { get; set; }

        public string? Email { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace(Email);
    }
}