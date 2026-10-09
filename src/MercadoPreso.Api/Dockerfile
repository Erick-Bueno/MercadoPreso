# syntax=docker/dockerfile:1.7-labs
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5113

ENV ASPNETCORE_URLS=http://+:5113

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY  --parents src/**/*.csproj ./
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["Directory.Packages.props", "Directory.Packages.props"]
RUN dotnet restore "src/MercadoPreso.Api/MercadoPreso.Api.csproj" 
COPY . .
RUN dotnet build "src/MercadoPreso.Api/MercadoPreso.Api.csproj" -c $configuration -o /app/build

FROM build As migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations bundle --verbose \
--project src/Modules/Catalog/Modules.Catalog.Infrastructure \
--startup-project src/MercadoPreso.Api \
--context CatalogDbContext \
--self-contained -r linux-x64 \
--output artifacts/migrations/efbundle-catalog

FROM mcr.microsoft.com/dotnet/runtime-deps:10.0 AS migrator
COPY --from=migrations /src/artifacts/migrations/efbundle-catalog /migrations/efbundle-catalog
ENTRYPOINT ["/migrations/efbundle-catalog"]

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "src/MercadoPreso.Api/MercadoPreso.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MercadoPreso.Api.dll"]
