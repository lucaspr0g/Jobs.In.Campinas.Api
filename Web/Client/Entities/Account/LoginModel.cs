using System.ComponentModel.DataAnnotations;

namespace Web.Client.Entities.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o email.")]
		[EmailAddress(ErrorMessage = "Digite um email válido.")]
		[Display(Name = "Email")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Digite a senha.")]
		[StringLength(30, ErrorMessage = "A senha deve conter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}