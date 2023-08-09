using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IJobRepository : IBaseRepository<JobDto>
    {
        Task GetUserJobsAsync(string userId);
        Task<JobDto> CreateAsync(JobDto dto, string userId);
    }
}