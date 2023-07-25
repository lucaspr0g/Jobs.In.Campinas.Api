using CrossCutting.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastucture.Repository.Repositories
{
    public abstract class BaseRepository<Source, Destination>
    {
        protected readonly IMongoCollection<Source> _collection;

        public BaseRepository(IOptions<AppSettings> options, string collectionName)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);

            _collection = database.GetCollection<Source>(collectionName);
        }
    }
}