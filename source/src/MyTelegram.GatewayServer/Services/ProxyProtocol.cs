using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MyTelegram.GatewayServer.Services;

public class ProxyProtocol
{
    private static readonly IProxyProtocolParser ProxyProtocolParser = new ProxyProtocolParser();

    public static async Task ProcessAsync(ConnectionContext connection, Func<Task> next, ILogger logger)
    {
        var input = connection.Transport.Input;
        while (!connection.ConnectionClosed.IsCancellationRequested)
        {
            var result = await input.ReadAsync();
            if (result.IsCanceled)
            {
                break;
            }

            var buffer = result.Buffer;
            if (buffer.Length == 0)
            {
                continue;
            }

            try
            {
                // The proxy protocol marker min length is 12
                if (buffer.Length < 12)
                {
                    continue;
                }

                if (!ProxyProtocolParser.IsProxyProtocolV2(buffer))
                {
                    break;
                }

                if (!ProxyProtocolParser.HasEnoughProxyProtocolV2Data(buffer, out _))
                {
                    continue;
                }

                var proxyProtocolFeature = ProxyProtocolParser.Parse(ref buffer);
                if (proxyProtocolFeature != null)
                {
                    connection.Features.Set(proxyProtocolFeature);
                }
                else
                {
                    var localEndPoint = connection.LocalEndPoint as IPEndPoint;
                    var remoteEndPoint = connection.RemoteEndPoint as IPEndPoint;
                    logger.ParseProxyProtocolFailed(localEndPoint?.Address, localEndPoint?.Port, remoteEndPoint?.Address, remoteEndPoint?.Port);
                }

                break;
            }
            finally
            {
                input.AdvanceTo(buffer.Start);
            }
        }

        await next();
    }
}