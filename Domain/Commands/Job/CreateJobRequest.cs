using MediatR;

namespace Domain.Commands.Job
{
    public sealed class CreateJobRequest : IRequest<Unit>
    {
        public string Title { get; set; }

        public bool IsVald()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }
    }
}