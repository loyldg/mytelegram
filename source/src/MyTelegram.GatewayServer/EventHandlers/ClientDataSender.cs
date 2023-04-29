namespace MyTelegram.GatewayServer.EventHandlers;

public class ClientDataSender : IClientDataSender
{
    private readonly IClientManager _clientManager;
    private readonly ILogger<ClientDataSender> _logger;
    private readonly IMtpMessageEncoder _messageEncoder;

    public ClientDataSender(IClientManager clientManager,
        ILogger<ClientDataSender> logger,
        IMtpMessageEncoder messageEncoder)
    {
        _clientManager = clientManager;
        _logger = logger;
        _messageEncoder = messageEncoder;
    }

    public Task SendAsync(UnencryptedMessageResponse data)
    {
        if (!_clientManager.TryGetClientData(data.ConnectionId, out var d))
        {
            _logger.LogWarning("Can not find cached client info,connectionId={ConnectionId}", data.ConnectionId);
            return Task.CompletedTask;
        }

        var encodedBytes = ArrayPool<byte>.Shared.Rent(GetEncodedByteLength(data.Data.Length));
        try
        {
            var totalCount = _messageEncoder.Encode(d, data, encodedBytes);
            return SendAsync(encodedBytes.AsMemory()[..totalCount], d);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(encodedBytes);
        }
    }

    public Task SendAsync(EncryptedMessageResponse data)
    {
        if (!_clientManager.TryGetClientData(data.ConnectionId, out var d))
        {
            _logger.LogWarning("Can not find cached client info,connectionId={ConnectionId}", data.ConnectionId);
            return Task.CompletedTask;
        }

        var encodedBytes = ArrayPool<byte>.Shared.Rent(GetEncodedByteLength(data.Data.Length));
        try
        {
            var totalCount = _messageEncoder.Encode(d, data, encodedBytes);

            return SendAsync(encodedBytes.AsMemory()[..totalCount], d);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(encodedBytes);
        }
    }

    public async Task SendAsync(ReadOnlyMemory<byte> encodedBytes,
        ClientData clientData)
    {
        switch (clientData.ClientType)
        {
            case ClientType.Tcp:
                await clientData.ConnectionContext!.Transport.Output.WriteAsync(encodedBytes);
                await clientData.ConnectionContext!.Transport.Output.FlushAsync();
                break;

            case ClientType.WebSocket:
                await clientData.WebSocket!.SendAsync(encodedBytes, WebSocketMessageType.Binary, true, default);
                break;
        }
    }

    private int GetEncodedByteLength(int messageDataLength)
    {
        // lengthBytes,defaultAuthKeyIdBytes,messageIdBytes,messageDataLengthBytes
        return 1 + 8 + 8 + 4 + messageDataLength;
    }
}