using MediatR;

namespace Domain.Commands.Job
{
    public sealed class CreateJobRequest : IRequest<Unit>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Positions { get; set; }

        public bool IsVald()
        {
            return !string.IsNullOrWhiteSpace(Title) && 
                !string.IsNullOrWhiteSpace(Description) && 
                Positions > 0;
        }
    }
}