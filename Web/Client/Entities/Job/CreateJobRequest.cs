using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Job.Create
{
    public sealed class CreateJobRequest
    {
        [Required(ErrorMessage = "Preencha o título da vaga.")]
        [StringLength(50, ErrorMessage = "O título da vaga deve conter no máximo 50 caracteres.")]
        public string? Title { get; set; }

		[Required(ErrorMessage = "Preencha a descrição da vaga.")]
		[StringLength(250, MinimumLength = 20, ErrorMessage = "A descrição da vaga deve conter no mínimo 20 caracteres e no máximo 250.")]
		public string? Description { get; set; }

		[Required(ErrorMessage = "Preencha os requisítos da vaga.")]
		[StringLength(250, MinimumLength = 20, ErrorMessage = "Os requisitos da vaga deve conter no mínimo 20 caracteres e no máximo 250.")]
		public string? Requirements { get; set; }

		[Required(ErrorMessage = "Preencha o contato da vaga.")]
		[StringLength(100, ErrorMessage = "O contato vaga deve conter no máximo 100 caracteres.")]
		public string? Contact { get; set; }
 
		[Required(ErrorMessage = "Preencha o local da vaga.")]
		[StringLength(60, ErrorMessage = "O local da vaga deve conter no máximo 60 caracteres.")]
		public string? Location { get; set; }

		[Required(ErrorMessage = "Preencha o número de vagas disponíveis.")]
		[Range(1, 100, ErrorMessage = "A quantidade mínima de vagas é 1.")]
		public int Positions { get; set; } = 1;

		[Range(1, 9999, ErrorMessage = "O salário mínimo permitido é 1.")]
		public decimal? Salary { get; set; }
	}
}