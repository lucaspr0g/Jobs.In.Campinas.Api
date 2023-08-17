using MediatR;

namespace Domain.Queries.GetJobs
{
    public sealed class GetJobsQuery : IRequest<PaginationResponse>
    {
        public GetJobsQuery(int page)
        {
            Page = page;
        }

        public int? Page { get; set; }
	}
}