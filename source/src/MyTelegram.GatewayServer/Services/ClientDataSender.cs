namespace MyTelegram.GatewayServer.Services;

public class ClientDataSender(
    IClientManager clientManager,
    ILogger<ClientDataSender> logger,
    IMessageQueueProcessor<ClientDisconnectedEvent> messageQueueProcessor,
    IMtpMessageEncoder messageEncoder)
    : IClientDataSender, ITransientDependency
{
    public Task SendAsync(UnencryptedMessageResponse data)
    {
        if (!clientManager.TryGetClientData(data.ConnectionId, out var d))
        {
            logger.CachedClientInfoNotFound(data.ConnectionId);
            return Task.CompletedTask;
        }

        d.UnencryptedMessageResponseQueue?.Writer.WriteAsync(data);

        return Task.CompletedTask;
    }

    public Task SendAsync(EncryptedMessageResponse data)
    {
        if (!clientManager.TryGetClientData(data.ConnectionId, out var d))
        {
            if (!clientManager.TryGetClientData(data.AuthKeyId, out d))
            {
                logger.CachedClientInfoNotFound2(data.ConnectionId, data.AuthKeyId);
                messageQueueProcessor.Enqueue(new ClientDisconnectedEvent(data.ConnectionId, data.AuthKeyId, 0), 0);
                return Task.CompletedTask;
            }
        }

        d?.EncryptedMessageResponseQueue.Writer.TryWrite(data);

        return Task.CompletedTask;
    }

    public int EncodeData(EncryptedMessageResponse data, ClientData d, Memory<byte> encodedBytes)
    {
        //if (d.AuthKeyId == 0)
        //{
        //    d.AuthKeyId = data.AuthKeyId;
        //}

        //if (!clientManager.ContainsAuthKey(data.AuthKeyId))
        //{
        //    clientManager.UpdateAuthKeyId(data.AuthKeyId,data.ConnectionId);
        //}

        return messageEncoder.Encode(d, data, encodedBytes.Span);
    }

    public int EncodeData(UnencryptedMessageResponse data, ClientData d, Memory<byte> encodedBytes)
    {
        return messageEncoder.Encode(d, data, encodedBytes.Span);
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

    public int GetEncodedDataMaxLength(int messageDataLength)
    {
        // LengthBytes=1~19 Abridged:1/4 | Intermediate:4 | Padded intermediate:4+(0~15) | Full:12
        // length(use max length 20),authKeyId(8),messageId(8),messageDataLength(4),messageData
        return 20 + 8 + 8 + 4 + messageDataLength;
    }
}