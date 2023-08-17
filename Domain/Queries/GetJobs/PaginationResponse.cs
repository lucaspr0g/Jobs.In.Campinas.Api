using Domain.Entities;

namespace Domain.Queries.GetJobs
{
	public sealed class PaginationResponse
	{
        public PaginationResponse(IEnumerable<JobDto> jobs, long totalPages)
        {
            Jobs = jobs;
            TotalPages = totalPages;
        }

        public IEnumerable<JobDto> Jobs { get; set; }

        public long TotalPages { get; set; }
	}
}