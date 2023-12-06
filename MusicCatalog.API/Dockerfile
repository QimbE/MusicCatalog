FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MusicCatalog.API/MusicCatalog.API.csproj", "MusicCatalog.API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
RUN dotnet restore "MusicCatalog.API/MusicCatalog.API.csproj"
COPY . .
WORKDIR "/src/MusicCatalog.API"
RUN dotnet build "MusicCatalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicCatalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicCatalog.API.dll"]
