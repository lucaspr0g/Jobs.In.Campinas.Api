using CrossCutting.Configurations;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastucture.Repository.Collections;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastucture.Repository.Repositories
{
    public class JobRepository : IJobRepository
    {
        private const string _collectionName = "jobs";

        private readonly IMongoCollection<Job> _jobCollection;

        public JobRepository(IOptions<AppSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);

            _jobCollection = database.GetCollection<Job>(_collectionName);
        }

        public async Task<IEnumerable<GetJobModel>> GetAsync()
        {
            var jobs = await _jobCollection
                .Find(s => true)
                .ToListAsync();

            var list = new List<GetJobModel>(jobs.Count);

            foreach (var job in jobs)
            {
                list.Add(new GetJobModel
                {
                    Id = job.Id,
                    Title = job.Title
                });
            }

            return list;
        }

        public async Task<GetJobModel> GetAsync(string id)
        {
            var job = await _jobCollection
                .Find(s => s.Id == id)
                .FirstOrDefaultAsync();

            return new GetJobModel 
            { 
                Id = job.Id,
                Title = job.Title 
            };
        }

        public async Task CreateAsync(CreateJobModel model)
        {
            var job = new Job
            {
                Title = model.Title
            };

            await _jobCollection.InsertOneAsync(job);
        }

        public async Task UpdateAsync(string id, UpdateJobModel model)
        {
            var job = new Job { Title = model.Title };
            await _jobCollection.ReplaceOneAsync(s => s.Id == id, job);
        }

        public async Task RemoveAsync(string id) =>
            await _jobCollection.DeleteOneAsync(s => s.Id == id);
    }
}