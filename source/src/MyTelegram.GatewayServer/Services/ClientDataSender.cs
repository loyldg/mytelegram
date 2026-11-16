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

        // TryWrite never blocks on an unbounded channel and does not create a faulted task when the
        // channel has been completed because the connection is closing.
        d.UnencryptedMessageResponseQueue?.Writer.TryWrite(data);

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

    public Task SendAsync(QuickAckEvent data)
    {
        // A quick ACK only makes sense on the connection that received the payload, so there is no
        // fallback to another connection of the same auth key.
        if (!clientManager.TryGetClientData(data.ConnectionId, out var d))
        {
            logger.LogDebug("Quick ack {QuickAckToken:x8} dropped, the connection is closed. ConnectionId: {ConnectionId}",
                data.QuickAckToken,
                data.ConnectionId);
            return Task.CompletedTask;
        }

        var result = d.TransportState.QuickAck.TryAccept(data.QuickAckToken);
        if (result != QuickAckAcceptResult.Accepted)
        {
            logger.LogDebug("Quick ack {QuickAckToken:x8} dropped: {Reason}. ConnectionId: {ConnectionId}",
                data.QuickAckToken,
                result,
                data.ConnectionId);
            return Task.CompletedTask;
        }

        d.QuickAckQueue.Writer.TryWrite(data.QuickAckToken);

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

    public int EncodeQuickAck(uint quickAckToken, ClientData d, Memory<byte> encodedBytes)
    {
        return messageEncoder.EncodeQuickAck(d, quickAckToken, encodedBytes.Span);
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
                await clientData.WebSocket!.SendAsync(encodedBytes, WebSocketMessageType.Binary, true, CancellationToken.None);

                break;
        }
    }

    public int GetEncodedDataMaxLength(int messageDataLength)
    {
        // Transport overhead: Abridged 1/4 | Intermediate 4 | Padded intermediate 4 + (0~15) | Full 12
        // plus the unencrypted header: authKeyId(8), messageId(8), messageDataLength(4), messageData
        return MtpMessageEncoder.MaxTransportOverhead + 8 + 8 + 4 + messageDataLength;
    }
}