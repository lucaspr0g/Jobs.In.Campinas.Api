using Domain.Interfaces.Services;
using MediatR;

namespace Domain.Commands.Account.Create
{
    public sealed class AccountCreateCommandHandler : IRequestHandler<AccountCreateRequest, Unit>
    {
        private readonly IAccountService _accountService;

        public AccountCreateCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(AccountCreateRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new Exception("Dados inválidos.");

			await _accountService.CreateAsync(request);

			return Unit.Value;
        }
    }
}