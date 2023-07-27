using MediatR;

namespace Domain.Commands.Account.Logoff
{
    public sealed class LogoffRequest : IRequest<Unit> { }
}