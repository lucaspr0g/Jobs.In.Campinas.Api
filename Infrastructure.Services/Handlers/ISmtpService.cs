using System.Net.Mail;

namespace Infrastructure.Services.Handlers
{
	public interface ISmtpService
	{
		MailMessage BuildMessage(string to, string token);
		void SendEmail(MailMessage message);
	}
}