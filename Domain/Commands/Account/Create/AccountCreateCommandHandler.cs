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
                throw new ArgumentException("dados invalidos");

            return await _accountService.CreateAsync(request);
        }
    }
}