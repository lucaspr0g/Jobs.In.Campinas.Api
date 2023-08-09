using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Repository.Collections
{
    [CollectionName("users")]
    public sealed class ApplicationUser : MongoIdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public DateTime? ModifiedOn { get; set; }

        public List<JobInformation> JobsInformation { get; set; }
    }

    public sealed class JobInformation
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }
    }
}