# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Flight/Flight.Worker/Flight.Worker.csproj", "src/Services/Flight/Flight.Worker/"]
COPY ["src/Building blocks/NextDestiny.Core/NextDestiny.Core.csproj", "src/Building blocks/NextDestiny.Core/"]
COPY ["src/Services/Flight/Flight.Application/Flight.Application.csproj", "src/Services/Flight/Flight.Application/"]
COPY ["src/Services/Flight/Flight.Domain/Flight.Domain.csproj", "src/Services/Flight/Flight.Domain/"]
COPY ["src/Services/Flight/Flight.Infrastructure/Flight.Infrastructure.csproj", "src/Services/Flight/Flight.Infrastructure/"]
RUN dotnet restore "./src/Services/Flight/Flight.Worker/Flight.Worker.csproj"
COPY . .
WORKDIR "/src/src/Services/Flight/Flight.Worker"
RUN dotnet build "./Flight.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Flight.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flight.Worker.dll"]