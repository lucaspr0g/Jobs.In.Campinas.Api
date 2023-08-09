using Domain.Interfaces.Services;
using MediatR;

namespace Domain.Commands.Account.Create
{
    public sealed class AccountCreateCommandHandler : IRequestHandler<AccountCreateRequest, AccountCreateResponse>
    {
        private readonly IAccountService _accountService;

        public AccountCreateCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<AccountCreateResponse> Handle(AccountCreateRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new AccountCreateResponse(false, "dados invalidos");

            return await _accountService.CreateAsync(request);
        }
    }
}