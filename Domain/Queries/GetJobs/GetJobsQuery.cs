using Domain.Entities;
using MediatR;

namespace Domain.Queries.GetJobs
{
    public sealed class GetJobsQuery : IRequest<IEnumerable<JobDto>>
    {
    }
}