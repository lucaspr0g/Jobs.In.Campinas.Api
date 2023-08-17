using Domain.Interfaces.Repositories;
using MediatR;

namespace Domain.Queries.GetJobs
{
    public sealed class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, PaginationResponse>
    {
		private const int PageSize = 10;

		private readonly IJobRepository _jobRepository;

        public GetJobsQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<PaginationResponse> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            request.Page = (request.Page is null || request.Page < 1) ? 
                0 : request.Page - 1;

            var (jobs, totalPages) = await _jobRepository.GetAllAsync(request.Page.Value, PageSize);

            return new PaginationResponse(jobs, totalPages);
        }
    }
}