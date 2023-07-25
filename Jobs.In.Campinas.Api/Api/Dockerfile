#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Jobs.In.Campinas.Api/Jobs.In.Campinas.Api.csproj", "Jobs.In.Campinas.Api/"]
RUN dotnet restore "Jobs.In.Campinas.Api/Jobs.In.Campinas.Api.csproj"
COPY . .
WORKDIR "/src/Jobs.In.Campinas.Api"
RUN dotnet build "Jobs.In.Campinas.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jobs.In.Campinas.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jobs.In.Campinas.Api.dll"]