FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/TraknovaTelematics.API/TraknovaTelematics.API.csproj", "src/TraknovaTelematics.API/"]
COPY ["src/TraknovaTelematics.Core/TraknovaTelematics.Core.csproj", "src/TraknovaTelematics.Core/"]
COPY ["src/TraknovaTelematics.Infrastructure/TraknovaTelematics.Infrastructure.csproj", "src/TraknovaTelematics.Infrastructure/"]
RUN dotnet restore "src/TraknovaTelematics.API/TraknovaTelematics.API.csproj"
COPY . .
RUN dotnet build "src/TraknovaTelematics.API/TraknovaTelematics.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/TraknovaTelematics.API/TraknovaTelematics.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TraknovaTelematics.API.dll"]
