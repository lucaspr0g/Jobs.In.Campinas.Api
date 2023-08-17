using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;

namespace Domain.Queries.GetUserJobs
{
    public sealed class GetUserJobsQueryHandler : IRequestHandler<GetUserJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IAccountService _accountService;

		public GetUserJobsQueryHandler(IJobRepository jobRepository, IAccountService accountService)
        {
            _jobRepository = jobRepository;
            _accountService = accountService;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetUserJobsQuery request, CancellationToken cancellationToken)
        {
            var userId = _accountService.GetAuthenticatedUserId();

            return await _jobRepository.GetAllAsync(userId);
        }
    }
}