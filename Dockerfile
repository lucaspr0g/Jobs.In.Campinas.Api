#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["*.sln", "./"]
COPY ["Api/Api.csproj", "Api/"]
COPY ["CrossCutting.Configurations/CrossCutting.Configurations.csproj", "CrossCutting.Configurations/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure.IoC/Infrastructure.IoC.csproj", "Infrastructure.IoC/"]
COPY ["Infrastructure.Repository/Infrastructure.Repository.csproj", "Infrastructure.Repository/"]

RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jobs.In.Campinas.Api.dll"]