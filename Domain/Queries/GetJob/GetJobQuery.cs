using Domain.Entities;
using MediatR;

namespace Domain.Queries.GetJob
{
    public sealed class GetJobQuery : IRequest<JobDto>
    {
        public GetJobQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }
    }
}