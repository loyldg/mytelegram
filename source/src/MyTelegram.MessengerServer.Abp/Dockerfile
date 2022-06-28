#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
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
#COPY sgerrand.rsa.pub /etc/apk/keys/sgerrand.rsa.pub
#COPY glibc-2.35-r0.apk /app/glibc-2.35-r0.apk
RUN wget -q -O /etc/apk/keys/sgerrand.rsa.pub https://alpine-pkgs.sgerrand.com/sgerrand.rsa.pub && \
    wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/2.35-r0/glibc-2.35-r0.apk &&\
	apk add glibc-2.35-r0.apk
RUN dotnet restore  --runtime linux-musl-x64 "src/MyTelegram.MessengerServer.Abp/MyTelegram.MessengerServer.Abp.csproj"
COPY . .
WORKDIR "/src/src/MyTelegram.MessengerServer.Abp"
RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.ustc.edu.cn/g' /etc/apk/repositories &&\
# Install gcompat package error should be Ignored
	apk --no-cache add gcompat ||true
RUN dotnet build -r linux-musl-x64 "MyTelegram.MessengerServer.Abp.csproj" -c Release -o /app/build

FROM build AS publish
#RUN dotnet publish "MyTelegram.MessengerServer.Abp.csproj" -c Release -o /app/publish
RUN dotnet publish "MyTelegram.MessengerServer.Abp.csproj" \
-r linux-musl-x64 \
--no-restore \
--self-contained true \
-c Release \
-o /app/publish 

#FROM base AS final
FROM alpine as final
WORKDIR /app
RUN apk --no-cache add libstdc++   
COPY --from=publish /app/publish .
ENTRYPOINT [ "/app/MyTelegram.MessengerServer.Abp"]