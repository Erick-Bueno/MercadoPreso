FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5113

ENV ASPNETCORE_URLS=http://+:5113

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/MercadoPreso.Api/MercadoPreso.Api.csproj", "src/MercadoPreso.Api/"]
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["Directory.Packages.props", "Directory.Packages.props"]
RUN dotnet restore "src/MercadoPreso.Api/MercadoPreso.Api.csproj"
COPY . .
WORKDIR "/src/src/MercadoPreso.Api"
RUN dotnet build "MercadoPreso.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "MercadoPreso.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MercadoPreso.Api.dll"]
