using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Account.Login
{
    public sealed class LoginRequest : IRequest<LoginResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}