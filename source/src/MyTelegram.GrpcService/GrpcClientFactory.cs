using Grpc.Net.Client;

namespace MyTelegram.GrpcService;

public static class GrpcClientFactory
{
    private static IdGeneratorService.IdGeneratorServiceClient? _idGeneratorServiceClient;
    private static MediaService.MediaServiceClient? _mediaServiceClient;
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