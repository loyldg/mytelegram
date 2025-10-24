# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/MyTelegram.Messenger.QueryServer/MyTelegram.Messenger.QueryServer.csproj", "src/MyTelegram.Messenger.QueryServer/"]
COPY ["src/MyTelegram.Caching.Redis/MyTelegram.Caching.Redis.csproj", "src/MyTelegram.Caching.Redis/"]
COPY ["src/MyTelegram.Core/MyTelegram.Core.csproj", "src/MyTelegram.Core/"]
COPY ["src/MyTelegram.Abstractions/MyTelegram.Abstractions.csproj", "src/MyTelegram.Abstractions/"]
COPY ["src/MyTelegram.Domain.Shared/MyTelegram.Domain.Shared.csproj", "src/MyTelegram.Domain.Shared/"]
COPY ["src/MyTelegram.Schema/MyTelegram.Schema.csproj", "src/MyTelegram.Schema/"]
COPY ["src/MyTelegram.Domain/MyTelegram.Domain.csproj", "src/MyTelegram.Domain/"]
COPY ["src/MyTelegram.EventFlow/MyTelegram.EventFlow.csproj", "src/MyTelegram.EventFlow/"]
COPY ["src/MyTelegram.GrpcService/MyTelegram.GrpcService.csproj", "src/MyTelegram.GrpcService/"]
COPY ["src/MyTelegram.EventBus.Rebus/MyTelegram.EventBus.Rebus.csproj", "src/MyTelegram.EventBus.Rebus/"]
COPY ["src/MyTelegram.EventBus/MyTelegram.EventBus.csproj", "src/MyTelegram.EventBus/"]
COPY ["src/MyTelegram.Services/MyTelegram.Services.csproj", "src/MyTelegram.Services/"]
COPY ["src/MyTelegram.Messenger/MyTelegram.Messenger.csproj", "src/MyTelegram.Messenger/"]
COPY ["src/MyTelegram.Converters/MyTelegram.Converters.csproj", "src/MyTelegram.Converters/"]
COPY ["src/MyTelegram.ReadModel.Interfaces/MyTelegram.ReadModel.Interfaces.csproj", "src/MyTelegram.ReadModel.Interfaces/"]
COPY ["src/MyTelegram.QueryHandlers.MongoDB/MyTelegram.QueryHandlers.MongoDB.csproj", "src/MyTelegram.QueryHandlers.MongoDB/"]
COPY ["src/MyTelegram.Queries/MyTelegram.Queries.csproj", "src/MyTelegram.Queries/"]
COPY ["src/MyTelegram.ReadModel/MyTelegram.ReadModel.csproj", "src/MyTelegram.ReadModel/"]
COPY ["src/MyTelegram.ReadModel.MongoDB/MyTelegram.ReadModel.MongoDB.csproj", "src/MyTelegram.ReadModel.MongoDB/"]
COPY ["src/MyTelegram.EventFlow.MongoDB/MyTelegram.EventFlow.MongoDB.csproj", "src/MyTelegram.EventFlow.MongoDB/"]
COPY ["src/MyTelegram.ReadModel.InMemory/MyTelegram.ReadModel.InMemory.csproj", "src/MyTelegram.ReadModel.InMemory/"]
RUN dotnet restore "./src/MyTelegram.Messenger.QueryServer/MyTelegram.Messenger.QueryServer.csproj"
COPY . .
WORKDIR "/src/src/MyTelegram.Messenger.QueryServer"
RUN dotnet build "./MyTelegram.Messenger.QueryServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyTelegram.Messenger.QueryServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyTelegram.Messenger.QueryServer.dll"]