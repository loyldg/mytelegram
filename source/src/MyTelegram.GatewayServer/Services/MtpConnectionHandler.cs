namespace MyTelegram.GatewayServer.Services;

public class MtpConnectionHandler(
    IClientManager clientManager,
    IMtpMessageParser messageParser,
    IMtpMessageDispatcher messageDispatcher,
    ILogger<MtpConnectionHandler> logger,
    IClientDataSender clientDataSender,
    IMessageQueueProcessor<ClientDisconnectedEvent> messageQueueProcessor)
    : ConnectionHandler
{
    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        var remoteEndPoint = connection.RemoteEndPoint;
        var proxyProtocolFeature = connection.Features.Get<ProxyProtocolFeature>();
        var connectionTypeFeature = connection.Features.Get<ConnectionTypeFeature>();

        var clientIp = (connection.RemoteEndPoint as IPEndPoint)?.Address.ToString() ?? string.Empty;
        if (proxyProtocolFeature != null)
        {
            remoteEndPoint = new IPEndPoint(proxyProtocolFeature.SourceIp, proxyProtocolFeature.SourcePort);
            clientIp = proxyProtocolFeature.SourceIp.ToString();

            logger.NewClientConnectedWUsingProxyProtocolV2(
                connection.ConnectionId,
                connectionTypeFeature?.DcId,
                (connection.LocalEndPoint as IPEndPoint)?.Port,
                connectionTypeFeature?.ConnectionType,
                remoteEndPoint,
                connection.RemoteEndPoint,
                clientManager.GetOnlineCount());
        }
        else
        {
            logger.NewClientConnected(
                connection.ConnectionId,
                connectionTypeFeature?.DcId,
                (connection.LocalEndPoint as IPEndPoint)?.Port,
                connectionTypeFeature?.ConnectionType,
                remoteEndPoint,
                clientManager.GetOnlineCount());
        }

        var clientData = new ClientData
        {
            ConnectionContext = connection,
            ConnectionId = connection.ConnectionId,
            ClientType = ClientType.Tcp,
            ClientIp = clientIp,
            ConnectionType = connectionTypeFeature?.ConnectionType ?? ConnectionType.Generic,
            DcId = connectionTypeFeature?.DcId ?? 0,
        };
        clientManager.AddClient(connection.ConnectionId, clientData);

        connection.ConnectionClosed.Register(() =>
        {
            if (clientManager.TryRemoveClient(connection.ConnectionId, out _))
            {
                messageQueueProcessor.Enqueue(
                    new ClientDisconnectedEvent(clientData.ConnectionId, clientData.AuthKeyId, 0),
                    clientData.AuthKeyId);
            }

            logger.ClientDisconnected(
                connection.ConnectionId,
                connectionTypeFeature?.DcId,
                remoteEndPoint,
                clientData.AuthKeyId);
        });

        var processSendUnencryptedDataTask = ProcessSendUnencryptedDataAsync(clientData, connection);
        var processSendDataTask = ProcessSendDataAsync(clientData, connection);
        var processReceiveDataTask = ProcessReceiveDataAsync(clientData, connection);

        await Task.WhenAny(processSendUnencryptedDataTask, processSendDataTask, processReceiveDataTask);
    }

    private async Task ProcessReceiveDataAsync(ClientData clientData, ConnectionContext connection)
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

            if (!clientManager.TryGetClientData(connection.ConnectionId, out _))
            {
                //logger.LogWarning("Cannot find client data, connectionId: {ConnectionId}", connection.ConnectionId);
                break;
            }

            if (!clientData.IsFirstPacketParsed)
            {
                messageParser.ProcessFirstUnencryptedPacket(ref buffer, clientData);
            }

            while (TryParseMessage(ref buffer, clientData, out var mtpMessage))
            {
                await ProcessDataAsync(mtpMessage, clientData);
            }

            input.AdvanceTo(buffer.Start, buffer.End);
            if (result.IsCompleted || result.IsCanceled)
            {
                break;
            }
        }

        await input.CompleteAsync();
    }

    private async Task ProcessSendUnencryptedDataAsync(ClientData clientData,
        ConnectionContext connectionContext)
    {
        var queue = clientData.UnencryptedMessageResponseQueue;
        while (await queue.Reader.WaitToReadAsync() && !connectionContext.ConnectionClosed.IsCancellationRequested)
        {
            while (queue.Reader.TryRead(out var response))
            {
                try
                {
                    if (!clientManager.TryGetClientData(clientData.ConnectionId, out var d))
                    {
                        logger.CachedClientInfoNotFound(clientData.ConnectionId);
                        continue;
                    }

                    var encodedBytes = ArrayPool<byte>.Shared.Rent(clientDataSender.GetEncodedDataMaxLength(response.Data.Length));
                    try
                    {
                        var totalCount = clientDataSender.EncodeData(response, d, encodedBytes);
                        await SendAsync(encodedBytes.AsMemory()[..totalCount], connectionContext);
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(encodedBytes);
                    }
                }
                finally
                {
                    response.MemoryOwner?.Dispose();
                }
            }
        }
    }

    private async Task ProcessSendDataAsync(ClientData clientData, ConnectionContext connectionContext)
    {
        var queue = clientData.EncryptedMessageResponseQueue;
        while (await queue.Reader.WaitToReadAsync() && !connectionContext.ConnectionClosed.IsCancellationRequested)
        {
            while (queue.Reader.TryRead(out var response))
            {
                try
                {
                    using var memoryOwner =
                        MemoryPool<byte>.Shared.Rent(clientDataSender.GetEncodedDataMaxLength(response.Data.Length));
                    var encodedBytes = memoryOwner.Memory;
                    clientManager.UpdateAuthKeyId(clientData, response.AuthKeyId, clientData.ConnectionId);
                    var totalCount = clientDataSender.EncodeData(response, clientData, encodedBytes);

                    await SendAsync(encodedBytes[..totalCount], connectionContext);
                }
                finally
                {
                    response.MemoryOwner?.Dispose();
                }
            }
        }
    }

    private async Task SendAsync(ReadOnlyMemory<byte> data, ConnectionContext connectionContext)
    {
        await connectionContext.Transport.Output.WriteAsync(data);
        await connectionContext.Transport.Output.FlushAsync();
    }

    private Task ProcessDataAsync(IMtpMessage mtpMessage,
        ClientData clientData)
    {
        mtpMessage.ConnectionType = clientData.ConnectionType;
        mtpMessage.DcId = clientData.DcId;

        if (clientData.IsFirstPacketParsed)
        {
            mtpMessage.ConnectionId = clientData.ConnectionId;
            mtpMessage.ClientIp = clientData.ClientIp;
            return messageDispatcher.DispatchAsync(mtpMessage);
        }

        return Task.CompletedTask;
    }

    private bool TryParseMessage(ref ReadOnlySequence<byte> buffer,
        ClientData clientData,
        [NotNullWhen(true)] out IMtpMessage? mtpMessage)
    {
        if (buffer.Length == 0)
        {
            mtpMessage = null;
            return false;
        }

        var reader = new SequenceReader<byte>(buffer);

        if (reader.Remaining < 4)
        {
            mtpMessage = null;

            return false;
        }

        return messageParser.TryParse(ref buffer, clientData, out mtpMessage);
    }
}