using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Repository.Collections
{
	[CollectionName("users")]
    public sealed class ApplicationUser : MongoIdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public DateTime? ModifiedOn { get; set; }
	}
}