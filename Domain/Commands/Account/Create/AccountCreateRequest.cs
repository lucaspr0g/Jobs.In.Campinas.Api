using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Account.Create
{
    public sealed class AccountCreateRequest : IRequest<AccountCreateResponse>
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