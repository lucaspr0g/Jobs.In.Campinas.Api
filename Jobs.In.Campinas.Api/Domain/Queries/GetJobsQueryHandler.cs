using Domain.Entities;  
using Domain.Interfaces.Repositories;
using MediatR;

namespace Domain.Queries
{
    public sealed class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, IEnumerable<GetJobModel>>
    {
        private readonly IJobRepository _jobRepository;

        public GetJobsQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<GetJobModel>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            return await _jobRepository.GetAsync();
        }
    }
}