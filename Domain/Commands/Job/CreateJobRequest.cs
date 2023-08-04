using MediatR;

namespace Domain.Commands.Job
{
    public sealed class CreateJobRequest : IRequest<Unit>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Positions { get; set; }

        public string UserId { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title) && 
                !string.IsNullOrWhiteSpace(Description) && 
                !string.IsNullOrWhiteSpace(UserId) &&
                Positions > 0;
        }
    }
}