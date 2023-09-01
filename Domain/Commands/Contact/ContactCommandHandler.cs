using Domain.Interfaces.Services;
using MediatR;

namespace Domain.Commands.Contact
{
	public sealed class ContactCommandHandler : IRequestHandler<ContactRequest>
	{
		private readonly ISmtpService _smtpService;

        public ContactCommandHandler(ISmtpService smtpService)
        {
            _smtpService = smtpService;
        }

        public Task Handle(ContactRequest request, CancellationToken cancellationToken)
		{
			if (!request.IsValid())
				throw new ArgumentException("Dados inválidos.");

			var message = _smtpService.BuildMessage(request.Email!, request.Subject!, request.Message!);
			_smtpService.SendEmail(message, cancellationToken);

			return Task.CompletedTask;
		}
	}
}