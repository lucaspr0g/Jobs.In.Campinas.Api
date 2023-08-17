using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Domain.Queries.GetJob
{
    public sealed class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobDto>
    {
        private readonly IJobRepository _jobRepository;

        public GetJobQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<JobDto> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("dados invalidos");

            return await _jobRepository.GetAsync(request.Id);
        }
    }
}