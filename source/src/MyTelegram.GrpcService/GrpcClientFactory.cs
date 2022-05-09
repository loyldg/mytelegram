using Grpc.Net.Client;

namespace MyTelegram.GrpcService;

public static class GrpcClientFactory
{
    //private static ChatMemberLoaderService.ChatMemberLoaderServiceClient? _chatMemberLoaderServiceClient;
    private static IdGeneratorService.IdGeneratorServiceClient? _idGeneratorServiceClient;
    private static MediaService.MediaServiceClient? _mediaServiceClient;
    //private static ChatService.ChatServiceClient? _chatServiceClient;

    //public static ChatService.ChatServiceClient CreateChatServiceClient(string address)
    //{
    //    if (_chatServiceClient == null)
    //    {
    //        var handler = new SocketsHttpHandler
    //        {
    //            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
    //            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
    //            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
    //            EnableMultipleHttp2Connections = true
    //        };

    //        var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
    //        {
    //            HttpHandler = handler
    //        });

    //        _chatServiceClient = new ChatService.ChatServiceClient(channel);
    //    }

    //    return _chatServiceClient;
    //}

    //public static ChatMemberLoaderService.ChatMemberLoaderServiceClient CreateChatMemberLoaderServiceClient(string address)
    //{
    //    if (_chatMemberLoaderServiceClient == null)
    //    {
    //        var handler = new SocketsHttpHandler
    //        {
    //            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
    //            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
    //            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
    //            EnableMultipleHttp2Connections = true
    //        };

    //        var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
    //        {
    //            HttpHandler = handler
    //        });

    //        _chatMemberLoaderServiceClient = new ChatMemberLoaderService.ChatMemberLoaderServiceClient(channel);
    //    }

    //    return _chatMemberLoaderServiceClient;
    //}

    public static IdGeneratorService.IdGeneratorServiceClient CreateIdGeneratorServiceClient(string address)
    {
        if (_idGeneratorServiceClient == null)
        {
            var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                HttpHandler = CreateHttpHandler()
            });
            _idGeneratorServiceClient = new IdGeneratorService.IdGeneratorServiceClient(channel);
        }

        return _idGeneratorServiceClient;
    }

    public static MediaService.MediaServiceClient CreateMediaServiceClient(string address)
    {
        if (_mediaServiceClient == null)
        {
            var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                HttpHandler = CreateHttpHandler()
            });
            _mediaServiceClient = new MediaService.MediaServiceClient(channel);
        }

        return _mediaServiceClient;
    }

    private static HttpMessageHandler CreateHttpHandler()
    {
        return new SocketsHttpHandler
        {
            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            EnableMultipleHttp2Connections = true
        };
    }
}