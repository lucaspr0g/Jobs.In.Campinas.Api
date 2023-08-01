using Domain.Entities;
using Infrastructure.Repository.Collections;
using Mapster;

namespace Infrastructure.Repository.Adapters
{
    public sealed class JobAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Job, JobDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Positions, src => src.Positions)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CreatedOn, src => src.CreatedOn.ToString("dd/MM/yyyy"))
                .Map(dest => dest.Time, src => src.CreatedOn.ToString("HH:mm"));

            config.NewConfig<JobDto, Job>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Positions, src => src.Positions);
        }
    }
}