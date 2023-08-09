using Infrastructure.IoC;
using Infrastructure.Services.Entities;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);

builder.Services.AddApplication(tokenConfigurations);
builder.Services.AddSwagger();
builder.Services.AddMongoDbIdentity(builder.Configuration);
builder.Services.ConfigureAuthentication(tokenConfigurations);
builder.Services.ConfigureAuthorization();
builder.Services.AddMapster();
builder.Services.AddHealthChecks();

//builder.Services.AddCors(cfg =>
//{
//    cfg.AddPolicy("AllowedOrigins",
//        policy =>
//        {
//            policy.AllowAnyOrigin()
//                .AllowAnyMethod()
//                .AllowAnyHeader();
//        });
//});

var app = builder.Build();
app.MapHealthChecks("/health");

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //IdentityModelEventSource.ShowPII = true;
}

app.UseHttpsRedirection();

//app.UseCors();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();