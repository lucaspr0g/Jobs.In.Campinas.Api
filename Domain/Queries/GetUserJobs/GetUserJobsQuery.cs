using Domain.Entities;
using MediatR;

namespace Domain.Queries.GetUserJobs
{
    public sealed class GetUserJobsQuery : IRequest<IEnumerable<JobDto>>
    {
    }
}