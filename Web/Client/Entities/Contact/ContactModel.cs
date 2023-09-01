using System.ComponentModel.DataAnnotations;

namespace Web.Client.Entities.Contact
{
	public sealed class ContactModel
	{
		[Required(ErrorMessage = "Digite o nome.")]
		[StringLength(50, ErrorMessage = "O nome deve conter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
		[Display(Name = "Name")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Digite o email.")]
		[EmailAddress(ErrorMessage = "Digite um email válido.")]
		[Display(Name = "Email")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Digite o assunto.")]
		[Display(Name = "Subject")]
		public string? Subject { get; set; }

		[Required(ErrorMessage = "Digite a mensagem.")]
		[Display(Name = "Message")]
		public string? Message { get; set; }

		public void Clear()
		{
			Name = string.Empty;
			Email = string.Empty;
			Subject = string.Empty;
			Message = string.Empty;
		}
	}
}