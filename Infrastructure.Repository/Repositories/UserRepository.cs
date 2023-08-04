using Domain.Entities;
using Infrastructure.Repository.Collections;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository.Repositories
{
    public sealed class UserRepository : BaseRepository<ApplicationUser, UserDto>
    {
        private const string _collectionName = "users";

        public UserRepository(IConfiguration configuration) : base(configuration, _collectionName) { }

        public async Task AddJob()
        {

        }
    }
}
