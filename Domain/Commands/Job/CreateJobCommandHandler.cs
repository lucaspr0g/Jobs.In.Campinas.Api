using Domain.Entities;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Domain.Commands.Job
{
    public sealed class CreateJobCommandHandler : IRequestHandler<CreateJobRequest, Unit>
    {
        private readonly IJobRepository _repository;

        public CreateJobCommandHandler(IJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateJobRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsVald())
                throw new ArgumentException("dados invalidos");

            var dto = request.Adapt<JobDto>();
            await _repository.CreateAsync(dto);
            return Unit.Value;
        }
    }
}