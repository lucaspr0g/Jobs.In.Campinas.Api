using Domain.Interfaces.Services;
using MediatR;

namespace Domain.Commands.Account.Confirm
{
	public sealed class ConfirmCommandHandler : IRequestHandler<ConfirmRequest, Unit>
	{
		private readonly IAccountService _accountService;

        public ConfirmCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(ConfirmRequest request, CancellationToken cancellationToken)
		{
			if (!request.IsValid())
				throw new ArgumentException("Dados de confirmação inválidos.");

			await _accountService.ConfirmEmailAsync(request.Email!, request.Token!);

			return Unit.Value;
		}
	}
}