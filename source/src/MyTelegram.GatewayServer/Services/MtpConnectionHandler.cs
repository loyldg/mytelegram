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

        logger.LogInformation(
            "{ConnectionId}:{RemoteEndpoint}",
            connection.ConnectionId,
            remoteEndPoint);

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

            var processReceiveDataTask =
                ProcessReceiveDataAsync(
                    clientData,
                    connection,
                    cancellationToken);

            var tasks = new[]
            {
                processSendUnencryptedDataTask,
                processSendDataTask,
                processReceiveDataTask
            };

            // If any worker exits, stop the other workers.
            await Task.WhenAny(tasks)
                .ConfigureAwait(false);

            await StopConnectionAsync(
                    connection,
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

        logger.ClientDisconnected(
            connection.ConnectionId,
            connectionTypeFeature?.DcId,
            remoteEndPoint,
            clientData.AuthKeyId);
    }

    private static async Task StopConnectionAsync(
        ConnectionContext connection,
        Task[] tasks)
    {
        // Closing the transport causes:
        //
        // PipeReader.ReadAsync()
        // ChannelReader.WaitToReadAsync()
        // Output.WriteAsync()
        // Output.FlushAsync()
        //
        // to finish/cancel.
        //
        // ConnectionClosed is also triggered as part of connection shutdown.

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
                        messageParser.ProcessFirstUnencryptedPacket(
                            ref buffer,
                            clientData);
                    }

                    while (TryParseMessage(
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

    private bool TryParseMessage(
        ref ReadOnlySequence<byte> buffer,
        ClientData clientData,
        [NotNullWhen(true)] out IMtpMessage? mtpMessage)
    {
        if (buffer.Length == 0)
        {
            mtpMessage = null;
            return false;
        }

        var reader = new SequenceReader<byte>(
            buffer);

        if (reader.Remaining < 4)
        {
            mtpMessage = null;
            return false;
        }

        return messageParser.TryParse(
            ref buffer,
            clientData,
            out mtpMessage);
    }
}