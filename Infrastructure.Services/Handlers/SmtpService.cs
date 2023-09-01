using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Infrastructure.Services.Handlers
{
	public sealed class SmtpService : ISmtpService
	{
		private readonly string Subject = "Confirmação de email - Campinas Jobs";
		private readonly string Body = "Para confirmar seu email, clique no link: ";

		private readonly string _email;
		private readonly string _server;
		private readonly string _password;
		private readonly string _domain;
		private readonly int _port;

        public SmtpService(IConfiguration configuration)
        {
			_domain = configuration.GetSection("SmtpConfiguration")["FrontDomain"]!;
			_email = configuration.GetSection("SmtpConfiguration")["From"]!;
			_server = configuration.GetSection("SmtpConfiguration")["Server"]!;
			_password = configuration.GetSection("SmtpConfiguration")["Password"]!;
			_port = Convert.ToInt32(configuration.GetSection("SmtpConfiguration")["Port"]);
		}

		public MailMessage BuildMessage(string from, string subject, string body)
		{
			var message = new MailMessage(_email, _email, subject, body);
			message.CC.Add(from);

			return message;
		}

		public MailMessage BuildMessage(string to, string token)
		{
			var htmlToken = HttpUtility.UrlEncode(token);
			var link = $"{_domain}/conta/confirmar?token={htmlToken}&email={to}";

			return new MailMessage(_email, to, Subject, string.Concat(Body, link));
		}

		public void SendEmail(MailMessage message, CancellationToken cancellationToken)
		{
			var client = new SmtpClient(_server, _port)
			{
				Credentials = new NetworkCredential(_email, _password),
				EnableSsl = true
			};

			try
			{
				client.SendAsync(message, cancellationToken);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}