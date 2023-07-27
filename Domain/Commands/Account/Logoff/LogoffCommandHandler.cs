using Domain.Interfaces.Services;
using MediatR;

namespace Domain.Commands.Account.Logoff
{
    public sealed class LogoffCommandHandler : IRequestHandler<LogoffRequest, Unit>
    {
        private readonly IAccountService _accountService;

        public LogoffCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(LogoffRequest request, CancellationToken cancellationToken)
        {
            await _accountService.LogoffAsync();
            return Unit.Value;
        }
    }
}