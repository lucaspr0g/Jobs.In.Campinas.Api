using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Domain.Queries.GetUserJobs
{
    public sealed class GetUserJobsQueryHandler : IRequestHandler<GetUserJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;

        public GetUserJobsQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetUserJobsQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("dados inválidos");

            return await _jobRepository.GetAllAsync();
        }
    }
}