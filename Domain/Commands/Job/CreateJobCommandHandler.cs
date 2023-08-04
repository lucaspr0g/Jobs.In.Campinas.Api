using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Mapster;
using MediatR;

namespace Domain.Commands.Job
{
    public sealed class CreateJobCommandHandler : IRequestHandler<CreateJobRequest, Unit>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IAccountService _accountService;

        public CreateJobCommandHandler(IJobRepository jobRepository, IAccountService accountService)
        {
            _jobRepository = jobRepository;
            _accountService = accountService;
        }

        public async Task<Unit> Handle(CreateJobRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("dados invalidos");

            var jobDto = request.Adapt<JobDto>();
            jobDto = await _jobRepository.CreateAsync(jobDto);

            var user = _accountService.GetAuthenticatedUser(request.UserId);

            return Unit.Value;
        }
    }
}