using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repository.Repositories
{
    public abstract class BaseRepository<Source, Destination>
    {
        protected readonly IMongoCollection<Source> _collection;

        public BaseRepository(IConfiguration configuration, string collectionName)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(configuration.GetSection("DatabaseName").Value);

            _collection = database.GetCollection<Source>(collectionName);
        }
    }
}