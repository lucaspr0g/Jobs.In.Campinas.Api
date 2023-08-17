using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Repository.Collections
{
    [CollectionName("roles")]
    public sealed class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
}