using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Mapster;
using MediatR;

namespace Domain.Commands.Job.Create
{
    public sealed class UpdateJobCommandHandler : IRequestHandler<CreateJobRequest, Unit>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IAccountService _accountService;

        public UpdateJobCommandHandler(IJobRepository jobRepository, IAccountService accountService)
        {
            _jobRepository = jobRepository;
            _accountService = accountService;

        }

        public async Task<Unit> Handle(CreateJobRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("dados invalidos");

            var dto = request.Adapt<JobDto>();
            var userId = _accountService.GetAuthenticatedUserId();

            await _jobRepository.CreateAsync(dto, userId);
            //await _userRepository.AddJobInformation(new Guid(userId), jobDto);

            return Unit.Value;
        }
    }
}