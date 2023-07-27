using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Services.Entities
{
    public sealed class TokenConfigurations
    {
        public string? Audience { get; set; }

        public string? Domain { get; set; }

        public string? Issuer { get; set; }

        public int Seconds { get; set; }

        public string JwtSecret { get; set; } = string.Empty;

        public SecurityKey SecurityKey => new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtSecret));
    }
}