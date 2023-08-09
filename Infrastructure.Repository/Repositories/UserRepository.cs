using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Repository.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repository.Repositories
{
    public sealed class UserRepository : BaseRepository<ApplicationUser, UserDto>, IUserRepository
    {
        private const string _collectionName = "users";

        public UserRepository(IConfiguration configuration) : base(configuration, _collectionName) { }

        //public async Task AddJobInformation(Guid userId, JobDto dto)
        //{
        //    var filter = Builders<ApplicationUser>
        //        .Filter
        //        .Eq(user => user.Id, userId);

        //    var user = _collection.Find(filter)
        //        .First();

        //    var oldJobsInformation = user.JobsInformation;

        //    if (user.JobsInformation is null)
        //        user.JobsInformation = new List<JobInformation>(1);

        //    user.JobsInformation.Add(new JobInformation
        //    {
        //        CreatedOn = Convert.ToDateTime(dto.CreatedOn),
        //        Id = dto.Id,
        //        Title = dto.Title
        //    });

        //    var update = Builders<ApplicationUser>
        //        .Update
        //        .Set(user => user.JobsInformation, user.JobsInformation);

        //    await _collection.UpdateOneAsync(filter, update);
        //}

        //public async Task AddJobInformation(Guid userId, JobDto dto)
        //{
        //    var filter = Builders<ApplicationUser>
        //        .Filter
        //        .Eq(user => user.Id, userId);

        //    var user = _collection.Find(filter)
        //        .First();

        //    var oldJobsInformation = user.JobsInformation;

        //    if (user.JobsInformation is null)
        //        user.JobsInformation = new List<JobInformation>(1);

        //    user.JobsInformation.Add(new JobInformation
        //    {
        //        CreatedOn = Convert.ToDateTime(dto.CreatedOn),
        //        Id = dto.Id,
        //        Title = dto.Title
        //    });

        //    var update = Builders<ApplicationUser>
        //        .Update
        //        .Set(user => user.JobsInformation, user.JobsInformation);

        //    await _collection.UpdateOneAsync(filter, update);
        //}
    }
}