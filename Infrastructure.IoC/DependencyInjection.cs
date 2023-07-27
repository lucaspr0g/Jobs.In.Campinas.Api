using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Queries;
using Infrastructure.Repository.Adapters;
using Infrastructure.Repository.Collections;
using Infrastructure.Repository.Repositories;
using Infrastructure.Services.Entities;
using Infrastructure.Services.Handlers;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, TokenConfigurations tokenConfigurations)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetJobsQueryHandler).Assembly);
            });

            services.AddSingleton(tokenConfigurations);

            services.AddScoped<IJobRepository, JobRepository>();

            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization Header\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddMongoDbIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(configuration.GetConnectionString("Mongodb"), configuration["DatabaseName"])
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, TokenConfigurations tokenConfigurations)
        {
            services.AddAuthentication()
                 .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                 {
                     options.Authority = $"https://{tokenConfigurations.Domain}";
                     options.SaveToken = true;
                     options.Configuration = new OpenIdConnectConfiguration();
                     options.TokenValidationParameters =
                       new TokenValidationParameters
                       {
                           ValidAudience = tokenConfigurations.Audience,
                           ValidIssuer = tokenConfigurations.Issuer,
                           IssuerSigningKey = tokenConfigurations.SecurityKey,
                           ValidateIssuerSigningKey = true, // Valida a assinatura de um token recebido
                           ValidateLifetime = true, // Verifica se um token recebido ainda é válido
                           ClockSkew = TimeSpan.Zero
                       };
                 });


            return services;
        }

        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());
            });

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