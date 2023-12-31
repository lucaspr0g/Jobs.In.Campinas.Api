﻿using Domain.Entities;
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
				.SortByDescending(s => s.CreatedOn)
				.ToListAsync();

			return jobs.Adapt<IEnumerable<JobDto>>();
		}

		public async Task<(IEnumerable<JobDto>, long)> GetAllAsync(int page, int size)
		{
			var totalPages = await _collection.CountDocumentsAsync(s => true) / size;

			var jobs = await _collection
				.Find(s => true)
				.SortByDescending(s => s.CreatedOn)
				.Skip(page * size)
				.Limit(size)
				.ToListAsync();

			return (jobs.Adapt<IEnumerable<JobDto>>(), totalPages);
		}

		public async Task<IEnumerable<JobDto>> GetAllAsync(string userId)
		{
			var jobs = await _collection
			.Find(s => s.UserId == userId)
			.SortByDescending(s => s.CreatedOn)
			.ToListAsync();

			return jobs.Adapt<IEnumerable<JobDto>>();

		}

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

		public async Task<JobDto> CreateAsync(JobDto dto, string userId)
		{
			var job = dto.Adapt<Job>();
			job.CreatedOn = DateTime.Now;
			job.UserId = userId;
			job.Status = "Opened";

			await _collection.InsertOneAsync(job);

			return job.Adapt<JobDto>();
		}

		public async Task UpdateAsync(string id, JobDto dto)
		{
			var filter = Builders<Job>
				.Filter
				.Eq(job => job.Id, id);

			var update = Builders<Job>
				.Update
				.Set(job => job.ModifiedOn, DateTime.Now)
				.Set(job => job.Title, dto.Title)
				.Set(job => job.Description, dto.Description)
				.Set(job => job.Positions, dto.Positions)
				.Set(job => job.Requirements, dto.Requirements)
				.Set(job => job.Contact, dto.Contact)
				.Set(job => job.Location, dto.Location)
				.Set(job => job.Salary, dto.Salary);

			await _collection.UpdateOneAsync(filter, update);
		}

		public async Task RemoveAsync(string id) =>
			await _collection.DeleteOneAsync(s => s.Id == id);
	}
}