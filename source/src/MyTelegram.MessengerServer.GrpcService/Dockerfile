#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine  AS build
WORKDIR /src
COPY ["src/MyTelegram.MessengerServer.GrpcService/MyTelegram.MessengerServer.GrpcService.csproj", "src/MyTelegram.MessengerServer.GrpcService/"]
COPY ["src/MyTelegram.Queries.MongoDB/MyTelegram.QueryHandlers.MongoDB.csproj", "src/MyTelegram.Queries.MongoDB/"]
COPY ["src/MyTelegram.ReadModel.MongoDB/MyTelegram.ReadModel.MongoDB.csproj", "src/MyTelegram.ReadModel.MongoDB/"]
COPY ["src/MyTelegram.Domain/MyTelegram.Domain.csproj", "src/MyTelegram.Domain/"]
COPY ["src/MyTelegram.Domain.Shared/MyTelegram.Domain.Shared.csproj", "src/MyTelegram.Domain.Shared/"]
COPY ["src/EventFlow.MongoDB/EventFlow.MongoDB.csproj", "src/EventFlow.MongoDB/"]
COPY ["src/MyTelegram.ReadModel/MyTelegram.ReadModel.csproj", "src/MyTelegram.ReadModel/"]
COPY ["src/MyTelegram.Queries/MyTelegram.Queries.csproj", "src/MyTelegram.Queries/"]
COPY ["src/MyTelegram.GrpcService/MyTelegram.GrpcService.csproj", "src/MyTelegram.GrpcService/"]
COPY ["src/MyTelegram.MessengerServer/MyTelegram.MessengerServer.csproj", "src/MyTelegram.MessengerServer/"]
COPY ["src/MyTelegram.Schema/MyTelegram.Schema.csproj", "src/MyTelegram.Schema/"]
COPY ["src/Microsoft.Cuckoo/Microsoft.Cuckoo.csproj", "src/Microsoft.Cuckoo/"]
COPY ["src/MyTelegram.Core/MyTelegram.Core.csproj", "src/MyTelegram.Core/"]
COPY sgerrand.rsa.pub /etc/apk/keys/sgerrand.rsa.pub
COPY glibc-2.35-r0.apk /app/glibc-2.35-r0.apk
RUN dotnet restore --runtime linux-musl-x64  "src/MyTelegram.MessengerServer.GrpcService/MyTelegram.MessengerServer.GrpcService.csproj"
COPY . .
WORKDIR "/src/src/MyTelegram.MessengerServer.GrpcService"

#ENV GLIBC_REPO=https://github.com/sgerrand/alpine-pkg-glibc
#ENV GLIBC_VERSION=2.35-r0
#RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories &&\
    #set -ex && \
    #apk --update add libstdc++ curl ca-certificates && \
    #for pkg in glibc-${GLIBC_VERSION} glibc-bin-${GLIBC_VERSION}; \
        #do curl -sSL ${GLIBC_REPO}/releases/download/${GLIBC_VERSION}/${pkg}.apk -o /tmp/${pkg}.apk; done && \
    #apk add --allow-untrusted /tmp/*.apk && \
    #rm -v /tmp/*.apk && \
    #/usr/glibc-compat/sbin/ldconfig /lib /usr/glibc-compat/lib
#RUN LD_PRELOAD=/lib/libgcompat.so ldd /root/.nuget/packages/grpc.tools/2.45.0/tools/linux_x64/protoc    

#RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories \
#&& apk --no-cache add libc6-compat \
#&& apk --no-cache add protobuf\
#&& cd /root/.nuget/packages/grpc.tools/2.45.0/tools/linux_x64 \
#&& rm protoc \
#&& ln -s /usr/bin/protoc protoc \
#&& chmod +x grpc_csharp_plugin

#RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories \
    #&& apk update \
    #&& apk --no-cache add libc6-compat \
    #&& apk --no-cache add protobuf \
    #&& cd /root/.nuget/packages/grpc.tools/2.46.3/tools/linux_x64 \
    #&& rm protoc \
    #&& ln -s /usr/bin/protoc protoc \
    #&& chmod +x grpc_csharp_plugin
#RUN ldd /usr/bin/protoc

RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories &&\
# Install gcompat package error should be Ignored
    apk --no-cache add /app/glibc-2.35-r0.apk && apk --no-cache add gcompat ||true

RUN dotnet build "MyTelegram.MessengerServer.GrpcService.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyTelegram.MessengerServer.GrpcService.csproj" \
-r linux-musl-x64 \
--no-restore \
--self-contained true \
-c Release \
-o /app/publish 

#FROM base AS final
FROM alpine AS final
WORKDIR /app
RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories &&\
    apk --no-cache add --no-cache libstdc++
COPY --from=publish /app/publish .
ENTRYPOINT ["/app/MyTelegram.MessengerServer.GrpcService"]