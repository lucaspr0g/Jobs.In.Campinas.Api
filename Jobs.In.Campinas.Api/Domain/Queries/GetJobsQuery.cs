using Domain.Entities;
using MediatR;

namespace Domain.Queries
{
    public sealed class GetJobsQuery : IRequest<IEnumerable<GetJobModel>>
    {
    }
}