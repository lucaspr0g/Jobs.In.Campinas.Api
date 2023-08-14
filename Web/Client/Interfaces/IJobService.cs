using Domain.Commands.Job.Create;
using Web.Client.Entities.Job;

namespace Web.Client.Interfaces
{
    public interface IJobService
    {
        Task<(bool, string?)> CreateJob(CreateJobRequest request);
		Task<JobResponse> GetJobs(int page);
        Task<JobEntity> GetJobById(string id);
        Task<IEnumerable<JobEntity>> GetUserJobs();
		Task<(bool, string?)> UpdateJob(UpdateJobRequest request);
        Task<(bool, string?)> DeleteJob(string id);
    }
}