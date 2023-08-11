using MediatR;

namespace Domain.Commands.Job.Delete
{
	public sealed class DeleteJobRequest : IRequest<Unit>
	{
        public DeleteJobRequest(string id)
        {
            Id = id;
        }

        public string? Id { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Id);
    }
}
