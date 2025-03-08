FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Flight/Flight.Worker/Flight.Worker.csproj", "Services/Flight/Flight.Worker/"]
COPY ["src/Building blocks/NextDestiny.Core/NextDestiny.Core.csproj", "Building blocks/NextDestiny.Core/"]
COPY ["src/Services/Flight/Flight.Application/Flight.Application.csproj", "Services/Flight/Flight.Application/"]
COPY ["src/Services/Flight/Flight.Domain/Flight.Domain.csproj", "Services/Flight/Flight.Domain/"]
COPY ["src/Services/Flight/Flight.Infrastructure/Flight.Infrastructure.csproj", "Services/Flight/Flight.Infrastructure/"]

RUN dotnet restore "Services/Flight/Flight.Worker/Flight.Worker.csproj"
COPY src/ .

WORKDIR /src/Services/Flight/Flight.Worker
RUN dotnet build "Flight.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Services/Flight/Flight.Worker
RUN dotnet publish "Flight.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flight.Worker.dll"]