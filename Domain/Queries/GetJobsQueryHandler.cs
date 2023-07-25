using Domain.Entities;  
using Domain.Interfaces.Repositories;
using MediatR;

namespace Domain.Queries
{
    public sealed class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;

        public GetJobsQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            return await _jobRepository.GetAllAsync();
        }
    }
}