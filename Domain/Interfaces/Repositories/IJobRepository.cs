using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IJobRepository : IBaseRepository<JobDto>
    {
        Task<JobDto> CreateAsync(JobDto dto, string userId);
        Task<IEnumerable<JobDto>> GetAllAsync(string userId);
        Task<(IEnumerable<JobDto>, long)> GetAllAsync(int page, int size);
	}
}