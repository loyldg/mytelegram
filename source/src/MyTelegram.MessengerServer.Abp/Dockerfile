#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MyTelegram.MessengerServer.Abp/MyTelegram.MessengerServer.Abp.csproj", "src/MyTelegram.MessengerServer.Abp/"]
COPY ["src/MyTelegram.Abp/MyTelegram.Abp.csproj", "src/MyTelegram.Abp/"]
COPY ["src/MyTelegram.Core/MyTelegram.Core.csproj", "src/MyTelegram.Core/"]
COPY ["src/MyTelegram.Domain.Shared/MyTelegram.Domain.Shared.csproj", "src/MyTelegram.Domain.Shared/"]
COPY ["src/MyTelegram.ReadModel.MongoDB/MyTelegram.ReadModel.MongoDB.csproj", "src/MyTelegram.ReadModel.MongoDB/"]
COPY ["src/MyTelegram.Domain/MyTelegram.Domain.csproj", "src/MyTelegram.Domain/"]
COPY ["src/EventFlow.MongoDB/EventFlow.MongoDB.csproj", "src/EventFlow.MongoDB/"]
COPY ["src/MyTelegram.ReadModel/MyTelegram.ReadModel.csproj", "src/MyTelegram.ReadModel/"]
COPY ["src/MyTelegram.Queries.MongoDB/MyTelegram.QueryHandlers.MongoDB.csproj", "src/MyTelegram.Queries.MongoDB/"]
COPY ["src/MyTelegram.Queries/MyTelegram.Queries.csproj", "src/MyTelegram.Queries/"]
COPY ["src/MyTelegram.GrpcService/MyTelegram.GrpcService.csproj", "src/MyTelegram.GrpcService/"]
COPY ["src/MyTelegram.MessengerServer/MyTelegram.MessengerServer.csproj", "src/MyTelegram.MessengerServer/"]
COPY ["src/MyTelegram.Schema/MyTelegram.Schema.csproj", "src/MyTelegram.Schema/"]
COPY ["src/Microsoft.Cuckoo/Microsoft.Cuckoo.csproj", "src/Microsoft.Cuckoo/"]
RUN dotnet restore "src/MyTelegram.MessengerServer.Abp/MyTelegram.MessengerServer.Abp.csproj"
COPY . .
WORKDIR "/src/src/MyTelegram.MessengerServer.Abp"
RUN dotnet build "MyTelegram.MessengerServer.Abp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyTelegram.MessengerServer.Abp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyTelegram.MessengerServer.Abp.dll"]