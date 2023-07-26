using CrossCutting.Configurations;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Repository.Collections;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repository.Repositories
{
    public class JobRepository : BaseRepository<JobModel, JobDto>, IJobRepository
    {
        private const string _collectionName = "jobs";

        public JobRepository(IOptions<AppSettings> options) : base(options, _collectionName) { }

        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var jobs = await _collection
                .Find(s => true)
                .ToListAsync();

            return jobs.Adapt<IEnumerable<JobDto>>();
        }

        public async Task<JobDto> GetAsync(string id)
        {
            var job = await _collection
                .Find(s => s.Id == id)
                .FirstOrDefaultAsync();

            return new JobDto
            {
                Id = job.Id,
                Title = job.Title
            };
        }

        public async Task CreateAsync(JobDto dto)
        {
            var job = new JobModel
            {
                Title = dto.Title
            };

            await _collection.InsertOneAsync(job);
        }

        public async Task UpdateAsync(string id, JobDto dto)
        {
            var job = new JobModel { Title = dto.Title };
            await _collection.ReplaceOneAsync(s => s.Id == id, job);
        }

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(s => s.Id == id);
    }
}