using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Repository.Collections;
using Mapster;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repository.Repositories
{
    public class JobRepository : BaseRepository<Job, JobDto>, IJobRepository
    {
        private const string _collectionName = "jobs";

        public JobRepository(IConfiguration configuration) : base(configuration, _collectionName) { }

        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var jobs = await _collection
                .Find(s => true)
                .ToListAsync();

            return jobs.Adapt<IEnumerable<JobDto>>();
        }

        //public async Task<IEnumerable<JobDto>> GetUserJobsAsync(string userId)
        //{
        //    var jobs = await _collection
        //        .Find(s => s.CreatedOn)
        //        .ToListAsync();

        //    return jobs.Adapt<IEnumerable<JobDto>>();
        //}

        public async Task<JobDto> GetAsync(string id)
        {
            var job = await _collection
                .Find(s => s.Id == id)
                .FirstOrDefaultAsync();

            return job.Adapt<JobDto>();
        }

        public async Task<JobDto> CreateAsync(JobDto dto)
        {
            var job = dto.Adapt<Job>();
            job.CreatedOn = DateTime.Now;

            await _collection.InsertOneAsync(job);

            return job.Adapt<JobDto>();
        }

        public async Task UpdateAsync(string id, JobDto dto)
        {
            var job = new Job { Title = dto.Title };
            await _collection.ReplaceOneAsync(s => s.Id == id, job);
        }

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(s => s.Id == id);

        public Task GetUserJobsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}