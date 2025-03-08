FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Order/Order.Api/Order.Api.csproj", "Services/Order/Order.Api/"]
COPY ["src/Services/Order/Order.Application/Order.Application.csproj", "Services/Order/Order.Application/"]
COPY ["src/Building blocks/NextDestiny.Core/NextDestiny.Core.csproj", "Building blocks/NextDestiny.Core/"]
COPY ["src/Services/Order/Order.Domain/Order.Domain.csproj", "Services/Order/Order.Domain/"]
COPY ["src/Services/Order/Order.Infrastructure/Order.Infrastructure.csproj", "Services/Order/Order.Infrastructure/"]

RUN dotnet restore "Services/Order/Order.Api/Order.Api.csproj"
COPY src/ .

WORKDIR /src/Services/Order/Order.Api
RUN dotnet build "./Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Services/Order/Order.Api
RUN dotnet publish "./Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]