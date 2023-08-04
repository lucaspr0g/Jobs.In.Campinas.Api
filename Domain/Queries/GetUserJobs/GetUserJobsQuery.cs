using Domain.Entities;
using MediatR;

namespace Domain.Queries.GetUserJobs
{
    public sealed class GetUserJobsQuery : IRequest<IEnumerable<JobDto>>
    {
        public GetUserJobsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; } = string.Empty;

        public bool IsValid() => !string.IsNullOrWhiteSpace(UserId);
    }
}