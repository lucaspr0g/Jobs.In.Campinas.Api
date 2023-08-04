using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Repository.Collections
{
    [CollectionName("users")]
    public sealed class ApplicationUser : MongoIdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public DateTime? ModifiedOn { get; set; }
    }
}