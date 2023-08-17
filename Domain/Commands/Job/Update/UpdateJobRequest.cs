using MediatR;

namespace Domain.Commands.Job.Update
{
    public sealed class UpdateJobRequest : IRequest<Unit>
    {
        public string? Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Positions { get; set; }

        public string? Requirements { get; set; }

        public string? Contact { get; set; }

        public string? Location { get; set; }

        public decimal? Salary { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Id) &&
                !string.IsNullOrWhiteSpace(Title) &&
                !string.IsNullOrWhiteSpace(Description) &&
                !string.IsNullOrWhiteSpace(Requirements) &&
                !string.IsNullOrWhiteSpace(Contact) &&
                !string.IsNullOrWhiteSpace(Location) &&
                Positions > 0 &&
                (Salary is null || Salary > 0);
        }
    }
}