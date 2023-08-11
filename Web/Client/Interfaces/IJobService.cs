using Domain.Commands.Job.Create;
using Web.Client.Entities.Job;

namespace Web.Client.Interfaces
{
    public interface IJobService
    {
        Task<(bool, string?)> CreateJob(CreateJobRequest request);
		Task<IEnumerable<JobResponse>> GetJobs();
        Task<JobResponse> GetJobById(string id);
        Task<IEnumerable<JobResponse>> GetUserJobs();
		Task<(bool, string?)> UpdateJob(UpdateJobRequest request);
        Task<(bool, string?)> DeleteJob(string id);
    }
}