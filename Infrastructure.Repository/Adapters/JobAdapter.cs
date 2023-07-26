using Domain.Entities;
using Infrastructure.Repository.Collections;
using Mapster;

namespace Infrastructure.Repository.Adapters
{
    public sealed class JobAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<JobModel, JobDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title);
        }
    }
}