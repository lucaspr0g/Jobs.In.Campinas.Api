using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IJobRepository
    {
        Task CreateAsync(CreateJobModel model);
        Task<IEnumerable<GetJobModel>> GetAsync();
        Task<GetJobModel> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, UpdateJobModel model);
    }
}