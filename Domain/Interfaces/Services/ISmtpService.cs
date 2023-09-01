using System.Net.Mail;

namespace Domain.Interfaces.Services
{
	public interface ISmtpService
	{
		MailMessage BuildMessage(string from, string subject, string body);
		MailMessage BuildMessage(string to, string token);
		void SendEmail(MailMessage message, CancellationToken cancellationToken);
	}
}