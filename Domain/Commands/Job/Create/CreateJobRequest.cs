using MediatR;

namespace Domain.Commands.Job.Create
{
    public sealed class CreateJobRequest : IRequest<Unit>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Positions { get; set; }

        public string? Requirements { get; set; }

        public string? Contact { get; set; }

        public string? Location { get; set; }

        public decimal? Salary { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title) &&
                !string.IsNullOrWhiteSpace(Description) &&
                Positions > 0 &&
                !string.IsNullOrWhiteSpace(Requirements) &&
                !string.IsNullOrWhiteSpace(Contact) &&
                !string.IsNullOrWhiteSpace(Location) &&
                (Salary is null || Salary > 0);
        }
    }
}