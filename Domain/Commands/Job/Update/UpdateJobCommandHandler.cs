using Domain.Entities;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Domain.Commands.Job.Update
{
    public sealed class UpdateJobCommandHandler : IRequestHandler<UpdateJobRequest, Unit>
    {
        private readonly IJobRepository _jobRepository;

        public UpdateJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Unit> Handle(UpdateJobRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("dados invalidos");

            var jobDto = request.Adapt<JobDto>();
            await _jobRepository.UpdateAsync(jobDto.Id, jobDto);

            return Unit.Value;
        }
    }
}