namespace MyTelegram.GatewayServer.Services;

public sealed class MtpConnectionHandler(
    IClientManager clientManager,
    IMtpMessageParser messageParser,
    IMtpMessageDispatcher messageDispatcher,
    ILogger<MtpConnectionHandler> logger,
    IClientDataSender clientDataSender,
    IHostApplicationLifetime applicationLifetime,
    IMessageQueueProcessor<ClientDisconnectedEvent> messageQueueProcessor)
    : ConnectionHandler
{
    public override async Task OnConnectedAsync(
        ConnectionContext connection)
    {
        var remoteEndPoint = connection.RemoteEndPoint;

        var proxyProtocolFeature =
            connection.Features.Get<ProxyProtocolFeature>();

        var connectionTypeFeature =
            connection.Features.Get<ConnectionTypeFeature>();

        var clientIp =
            (connection.RemoteEndPoint as IPEndPoint)
            ?.Address
            .ToString()
            ?? string.Empty;

        if (proxyProtocolFeature != null)
        {
            remoteEndPoint = new IPEndPoint(
                proxyProtocolFeature.SourceIp,
                proxyProtocolFeature.SourcePort);

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
            ConnectionType =
                connectionTypeFeature?.ConnectionType
                ?? ConnectionType.Generic,
            DcId = connectionTypeFeature?.DcId ?? 0
        };

        clientManager.AddClient(
            connection.ConnectionId,
            clientData);

        var connectionClosedRegistration =
            connection.ConnectionClosed.Register(() =>
            {
                OnConnectionClosed(
                    connection,
                    clientData,
                    connectionTypeFeature,
                    remoteEndPoint);
            });

        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(
                connection.ConnectionClosed,
                applicationLifetime.ApplicationStopping);

            var cancellationToken = cts.Token;

            var processSendUnencryptedDataTask =
                ProcessSendUnencryptedDataAsync(
                    clientData,
                    connection,
                    cancellationToken);

            var processSendDataTask =
                ProcessSendDataAsync(
                    clientData,
                    connection,
                    cancellationToken);

            var processSendQuickAckTask =
                ProcessSendQuickAckAsync(
                    clientData,
                    connection,
                    cancellationToken);

            var processReceiveDataTask =
                ProcessReceiveDataAsync(
                    clientData,
                    connection,
                    cancellationToken);

            var tasks = new[]
            {
                processSendUnencryptedDataTask,
                processSendDataTask,
                processSendQuickAckTask,
                processReceiveDataTask
            };

            // If any worker exits, stop the other workers.
            await Task.WhenAny(tasks)
                .ConfigureAwait(false);

            // Stop the remaining workers deterministically (for example after a transport error the
            // send loops would otherwise wait until Kestrel reports the connection as closed).
            await cts.CancelAsync()
                .ConfigureAwait(false);

            await StopConnectionAsync(
                    connection,
                    clientData,
                    tasks)
                .ConfigureAwait(false);
        }
        finally
        {
            await connectionClosedRegistration.DisposeAsync();
        }
    }

    private void OnConnectionClosed(
        ConnectionContext connection,
        ClientData clientData,
        ConnectionTypeFeature? connectionTypeFeature,
        EndPoint? remoteEndPoint)
    {
        if (clientManager.TryRemoveClient(
                connection.ConnectionId,
                out _))
        {
            messageQueueProcessor.Enqueue(
                new ClientDisconnectedEvent(
                    clientData.ConnectionId,
                    clientData.AuthKeyId,
                    0),
                clientData.AuthKeyId);
        }

        var pendingQuickAckCount = clientData.TransportState.QuickAck.Reset();
        if (pendingQuickAckCount > 0)
        {
            logger.LogDebug("[ConnectionId: {ConnectionId}] {Count} pending quick ack request(s) discarded on close",
                connection.ConnectionId,
                pendingQuickAckCount);
        }

        logger.ClientDisconnected(
            connection.ConnectionId,
            connectionTypeFeature?.DcId,
            remoteEndPoint,
            clientData.AuthKeyId);
    }

    private async Task StopConnectionAsync(
        ConnectionContext connection,
        ClientData clientData,
        Task[] tasks)
    {
        try
        {
            await connection.Transport.Input
                .CompleteAsync()
                .ConfigureAwait(false);
        }
        catch
        {
            // The transport may already be completed.
        }

        try
        {
            await connection.Transport.Output
                .CompleteAsync()
                .ConfigureAwait(false);
        }
        catch
        {
            // The transport may already be completed.
        }

        try
        {
            await Task.WhenAll(tasks)
                .ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // Expected when the connection is closed.
        }
        catch (IOException)
        {
            // Expected when the TCP connection is closed.
        }
        catch (ObjectDisposedException)
        {
            // Expected when the transport is already disposed.
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "[ConnectionId: {ConnectionId}] Connection worker failed",
                clientData.ConnectionId);
        }
    }

    private async Task ProcessReceiveDataAsync(
        ClientData clientData,
        ConnectionContext connection,
        CancellationToken cancellationToken)
    {
        var input = connection.Transport.Input;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                ReadResult result;

                try
                {
                    result = await input.ReadAsync(
                            cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var buffer = result.Buffer;

                try
                {
                    if (result.IsCanceled)
                    {
                        break;
                    }

                    if (buffer.Length == 0)
                    {
                        if (result.IsCompleted)
                        {
                            break;
                        }

                        continue;
                    }

                    if (!clientManager.TryGetClientData(
                            connection.ConnectionId,
                            out _))
                    {
                        break;
                    }

                    if (!clientData.IsFirstPacketParsed)
                    {
                        // Waits (consumes nothing) until enough bytes are received to detect the transport.
                        messageParser.ProcessFirstUnencryptedPacket(
                            ref buffer,
                            clientData);
                    }

                    // A single read may contain several frames (sticky packets) or only a part of one;
                    // the parser returns false and leaves the remaining bytes in the pipe until more data arrives.
                    while (messageParser.TryParse(
                               ref buffer,
                               clientData,
                               out var mtpMessage))
                    {
                        await ProcessDataAsync(
                                mtpMessage,
                                clientData)
                            .ConfigureAwait(false);
                    }

                    if (result.IsCompleted)
                    {
                        if (!buffer.IsEmpty)
                        {
                            logger.LogDebug(
                                "[ConnectionId: {ConnectionId}] Connection closed by the client with {Count} unprocessed byte(s)",
                                clientData.ConnectionId,
                                buffer.Length);
                        }

                        break;
                    }
                }
                finally
                {
                    input.AdvanceTo(
                        buffer.Start,
                        buffer.End);
                }
            }
        }
        catch (MtpTransportException ex)
        {
            // The byte stream cannot be resynchronized: close the connection.
            logger.LogWarning(
                "[ConnectionId: {ConnectionId}] Closing connection because of an MTProto transport error: {Message}",
                clientData.ConnectionId,
                ex.Message);
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (IOException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (ObjectDisposedException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "[ConnectionId: {ConnectionId}] Failed to process received data, closing connection",
                clientData.ConnectionId);
        }
        finally
        {
            try
            {
                await input.CompleteAsync()
                    .ConfigureAwait(false);
            }
            catch
            {
                // The transport may already be completed.
            }
        }
    }

    private async Task ProcessSendUnencryptedDataAsync(
        ClientData clientData,
        ConnectionContext connection,
        CancellationToken cancellationToken)
    {
        var queue =
            clientData.UnencryptedMessageResponseQueue;

        try
        {
            while (await queue.Reader.WaitToReadAsync(
                       cancellationToken)
                   .ConfigureAwait(false))
            {
                while (queue.Reader.TryRead(
                           out var response))
                {
                    try
                    {
                        if (!clientManager.TryGetClientData(
                                clientData.ConnectionId,
                                out var currentClientData))
                        {
                            logger.CachedClientInfoNotFound(
                                clientData.ConnectionId);

                            continue;
                        }

                        var maxLength =
                            clientDataSender
                                .GetEncodedDataMaxLength(
                                    response.Data.Length);

                        var encodedBytes =
                            ArrayPool<byte>.Shared.Rent(
                                maxLength);

                        try
                        {
                            var totalCount =
                                clientDataSender.EncodeData(
                                    response,
                                    currentClientData,
                                    encodedBytes);

                            await SendAsync(
                                    encodedBytes.AsMemory(
                                        0,
                                        totalCount),
                                    connection,
                                    cancellationToken)
                                .ConfigureAwait(false);
                        }
                        finally
                        {
                            ArrayPool<byte>.Shared.Return(
                                encodedBytes);
                        }
                    }
                    finally
                    {
                        response.MemoryOwner?.Dispose();
                    }
                }
            }
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (IOException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (ObjectDisposedException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    private async Task ProcessSendDataAsync(
        ClientData clientData,
        ConnectionContext connection,
        CancellationToken cancellationToken)
    {
        var queue =
            clientData.EncryptedMessageResponseQueue;

        try
        {
            while (await queue.Reader.WaitToReadAsync(
                       cancellationToken)
                   .ConfigureAwait(false))
            {
                while (queue.Reader.TryRead(
                           out var response))
                {
                    try
                    {
                        using var memoryOwner =
                            MemoryPool<byte>.Shared.Rent(
                                clientDataSender
                                    .GetEncodedDataMaxLength(
                                        response.Data.Length));

                        var encodedBytes =
                            memoryOwner.Memory;

                        clientManager.UpdateAuthKeyId(
                            clientData,
                            response.AuthKeyId,
                            clientData.ConnectionId);

                        var totalCount =
                            clientDataSender.EncodeData(
                                response,
                                clientData,
                                encodedBytes);

                        await SendAsync(
                                encodedBytes[..totalCount],
                                connection,
                                cancellationToken)
                            .ConfigureAwait(false);
                    }
                    finally
                    {
                        response.MemoryOwner?.Dispose();
                    }
                }
            }
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (IOException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (ObjectDisposedException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    private async Task ProcessSendQuickAckAsync(
        ClientData clientData,
        ConnectionContext connection,
        CancellationToken cancellationToken)
    {
        var queue =
            clientData.QuickAckQueue;
        var encodedBytes = new byte[MtpMessageEncoder.MaxQuickAckLength];

        try
        {
            while (await queue.Reader.WaitToReadAsync(
                       cancellationToken)
                   .ConfigureAwait(false))
            {
                while (queue.Reader.TryRead(
                           out var quickAckToken))
                {
                    //await clientData.SendLock.WaitAsync(cancellationToken)
                    //    .ConfigureAwait(false);
                    //try
                    //{
                    var totalCount =
                        clientDataSender.EncodeQuickAck(
                            quickAckToken,
                            clientData,
                            encodedBytes);

                    if (totalCount == 0)
                    {
                        continue;
                    }

                    await SendAsync(
                            encodedBytes.AsMemory(0, totalCount),
                            connection,
                            cancellationToken)
                        .ConfigureAwait(false);
                    //}
                    //finally
                    //{
                    //    clientData.SendLock.Release();
                    //}

                    logger.LogDebug(
                        "[ConnectionId: {ConnectionId}] Quick ack {QuickAckToken:x8} sent",
                        clientData.ConnectionId,
                        quickAckToken);
                }
            }
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (IOException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (ObjectDisposedException)
            when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    private static async Task SendAsync(
        ReadOnlyMemory<byte> data,
        ConnectionContext connection,
        CancellationToken cancellationToken)
    {
        await connection.Transport.Output
            .WriteAsync(
                data,
                cancellationToken)
            .ConfigureAwait(false);

        await connection.Transport.Output
            .FlushAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private Task ProcessDataAsync(
        IMtpMessage mtpMessage,
        ClientData clientData)
    {
        mtpMessage.ConnectionType =
            clientData.ConnectionType;

        mtpMessage.DcId =
            clientData.DcId;

        if (mtpMessage is EncryptedMessage { QuickAckRequested: true } encryptedMessage &&
            !clientData.TransportState.QuickAck.OnRequested())
        {
            // Too many unanswered requests: process the payload but do not acknowledge it.
            encryptedMessage.QuickAckRequested = false;
            logger.LogWarning(
                "[ConnectionId: {ConnectionId}] Too many pending quick ack requests, request ignored",
                clientData.ConnectionId);
        }

        if (clientData.IsFirstPacketParsed)
        {
            mtpMessage.ConnectionId =
                clientData.ConnectionId;

            mtpMessage.ClientIp =
                clientData.ClientIp;

            return messageDispatcher.DispatchAsync(
                mtpMessage);
        }

        return Task.CompletedTask;
    }
}