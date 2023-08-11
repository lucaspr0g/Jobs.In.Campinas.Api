using Domain.Interfaces.Repositories;
using MediatR;

namespace Domain.Commands.Job.Delete
{
	public sealed class DeleteJobCommandHandler : IRequestHandler<DeleteJobRequest, Unit>
	{
		private readonly IJobRepository _jobRepository;

        public DeleteJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;	
        }

        public async Task<Unit> Handle(DeleteJobRequest request, CancellationToken cancellationToken)
		{
			if (!request.IsValid())
				throw new ArgumentException("Dados inválidos.");

			await _jobRepository.RemoveAsync(request.Id!);

			return Unit.Value;
		}
	}
}