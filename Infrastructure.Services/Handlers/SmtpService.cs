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

		private readonly string _from;
		private readonly string _server;
		private readonly string _password;
		private readonly string _domain;
		private readonly int _port;

        public SmtpService(IConfiguration configuration)
        {
			_domain = configuration.GetSection("SmtpConfiguration")["FrontDomain"]!;
			_from = configuration.GetSection("SmtpConfiguration")["From"]!;
			_server = configuration.GetSection("SmtpConfiguration")["Server"]!;
			_password = configuration.GetSection("SmtpConfiguration")["Password"]!;
			_port = Convert.ToInt32(configuration.GetSection("SmtpConfiguration")["Port"]);
		}

		public MailMessage BuildMessage(string to, string token)
		{
			var htmlToken = HttpUtility.UrlEncode(token);
			var link = $"{_domain}/conta/confirmar?token={htmlToken}&email={to}";

			return new MailMessage(_from, to, Subject, string.Concat(Body, link));
		}

		public void SendEmail(MailMessage message)
		{
			var client = new SmtpClient(_server, _port)
			{
				Credentials = new NetworkCredential(_from, _password),
				EnableSsl = true
			};

			try
			{
				client.Send(message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}