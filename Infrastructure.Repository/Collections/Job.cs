using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Repository.Collections
{
    [CollectionName("jobs")]
    public sealed class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("userId")]
        public string? UserId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("requirements")]
        public string? Requirements { get; set; }

        [BsonElement("contact")]
        public string? Contact { get; set; }

        [BsonElement("location")]
        public string? Location { get; set; }

        [BsonElement("salary")]
        public decimal? Salary { get; set; }

        [BsonElement("positions")]
        public int Positions { get; set; }

        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; }

        [BsonElement("modifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }
    }
}