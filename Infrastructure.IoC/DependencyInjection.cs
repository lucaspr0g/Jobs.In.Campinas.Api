using Domain.Interfaces.Repositories;
using Domain.Queries;
using Infrastucture.Repository.Adapters;
using Infrastucture.Repository.Repositories;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetJobsQueryHandler).Assembly);
            });

            services.AddScoped<IJobRepository, JobRepository>();

            return services;
        }

        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

            var assemblies = new Assembly[]
            {
                typeof(JobAdapter).Assembly
            };

            typeAdapterConfig.Scan(assemblies);

            return services;
        }
    }
}