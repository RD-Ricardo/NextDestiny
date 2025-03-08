FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Hotel/Hotel.Cancellation.Worker/Hotel.Cancellation.Worker.csproj", "Services/Hotel/Hotel.Cancellation.Worker/"]
COPY ["src/Building blocks/NextDestiny.Core/NextDestiny.Core.csproj", "Building blocks/NextDestiny.Core/"]
COPY ["src/Services/Hotel/Hotel.Application/Hotel.Application.csproj", "Services/Hotel/Hotel.Application/"]
COPY ["src/Services/Hotel/Hotel.Domain/Hotel.Domain.csproj", "Services/Hotel/Hotel.Domain/"]
COPY ["src/Services/Hotel/Hotel.Infastructure/Hotel.Infastructure.csproj", "Services/Hotel/Hotel.Infastructure/"]

RUN dotnet restore "Services/Hotel/Hotel.Cancellation.Worker/Hotel.Cancellation.Worker.csproj"
COPY src/ .

WORKDIR /src/Services/Hotel/Hotel.Cancellation.Worker
RUN dotnet build "Hotel.Cancellation.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Services/Hotel/Hotel.Cancellation.Worker
RUN dotnet publish "Hotel.Cancellation.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotel.Cancellation.Worker.dll"]