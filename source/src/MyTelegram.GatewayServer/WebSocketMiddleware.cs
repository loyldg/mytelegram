namespace MyTelegram.GatewayServer;

public class WebSocketMiddleware(
    IMtpMessageParser messageParser,
    IMtpMessageDispatcher messageDispatcher,
    IClientManager clientManager,
    IClientDataSender clientDataSender,
    ILogger<WebSocketMiddleware> logger,
    IMessageQueueProcessor<ClientDisconnectedEvent> messageQueueProcessor)
    : IMiddleware, ITransientDependency
{
    private readonly string[] _allowedWsPaths = ["/apiws", "/apiw1", "/apiws_premium", "/apiws_test"];
    private readonly string _subProtocol = "binary";
    private bool _isWebSocketConnected;

    public async Task InvokeAsync(HttpContext context,
        RequestDelegate next)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            if (_allowedWsPaths.Contains(context.Request.Path.Value ?? string.Empty))
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync(_subProtocol);
                _isWebSocketConnected = true;

                var proxyProtocolFeature = context.Features.Get<ProxyProtocolFeature>();
                var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
                if (proxyProtocolFeature != null)
                {
                    clientIp = proxyProtocolFeature.SourceIp.ToString();
                }

                var connectionTypeFeature = context.Features.Get<ConnectionTypeFeature>();

                var clientData = new ClientData
                {
                    ConnectionId = context.Connection.Id,
                    WebSocket = webSocket,
                    ClientType = ClientType.WebSocket,
                    ClientIp = clientIp,
                    ConnectionType = connectionTypeFeature?.ConnectionType ?? ConnectionType.Generic,
                    DcId = connectionTypeFeature?.DcId ?? 0
                };
                clientManager.AddClient(context.Connection.Id, clientData);

                await ProcessWebSocketAsync(webSocket, clientData);
            }
            else
            {
                logger.RequestPathNotAllowed(context.Request.Path);
            }
        }
        else
        {
            await next(context);
        }
    }

    private async Task ProcessSendUnencryptedDataAsync(ClientData clientData)
    {
        var queue = clientData.UnencryptedMessageResponseQueue;
        while (await queue.Reader.WaitToReadAsync() && _isWebSocketConnected)
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
                        //await SendAsync(encodedBytes.AsMemory()[..totalCount], connectionContext);
                        await clientData.WebSocket!.SendAsync(encodedBytes.AsMemory()[..totalCount],
                            WebSocketMessageType.Binary, true, CancellationToken.None);
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

    private Task ProcessDataAsync(IMtpMessage mtpMessage,
        ClientData clientData)
    {
        if (mtpMessage is EncryptedMessage { QuickAckRequested: true } encryptedMessage &&
            !clientData.TransportState.QuickAck.OnRequested())
        {
            // Too many unanswered requests: process the payload but do not acknowledge it.
            encryptedMessage.QuickAckRequested = false;
            logger.LogWarning("[ConnectionId: {ConnectionId}] Too many pending quick ack requests, request ignored",
                clientData.ConnectionId);
        }

        if (clientData.IsFirstPacketParsed)
        {
            mtpMessage.ConnectionId = clientData.ConnectionId;
            mtpMessage.ClientIp = clientData.ClientIp;
            mtpMessage.DcId = clientData.DcId;
            mtpMessage.ConnectionType = clientData.ConnectionType;

            return messageDispatcher.DispatchAsync(mtpMessage);
        }

        return Task.CompletedTask;
    }

    private async Task ProcessWebSocketAsync(WebSocket webSocket,
        ClientData clientData)
    {
        var pipe = new Pipe();
        var writeTask = WritePipeAsync(webSocket, pipe.Writer);
        var readTask = ReadPipeAsync(pipe.Reader, clientData);
        var processResponseQueueTask = ProcessResponseQueueAsync(clientData);
        var processSendUnencryptedDataTask = ProcessSendUnencryptedDataAsync(clientData);
        var processSendQuickAckTask = ProcessSendQuickAckAsync(clientData);

        try
        {
            // The receive side ends when the client disconnects or sends invalid data.
            await Task.WhenAll(writeTask, readTask);
        }
        catch (Exception ex)
        {
            logger.LogDebug(ex, "[ConnectionId: {ConnectionId}] WebSocket receive loop ended with an error",
                clientData.ConnectionId);
        }

        _isWebSocketConnected = false;
        clientManager.RemoveClient(clientData.ConnectionId);

        // Wake up the send loops; without this they would wait for new responses forever.
        clientData.EncryptedMessageResponseQueue.Writer.TryComplete();
        clientData.UnencryptedMessageResponseQueue.Writer.TryComplete();
        clientData.QuickAckQueue.Writer.TryComplete();

        try
        {
            await Task.WhenAll(processSendUnencryptedDataTask, processResponseQueueTask, processSendQuickAckTask);
        }
        catch (Exception ex)
        {
            logger.LogDebug(ex, "[ConnectionId: {ConnectionId}] WebSocket send loop ended with an error",
                clientData.ConnectionId);
        }

        clientData.TransportState.QuickAck.Reset();
        messageQueueProcessor.Enqueue(new ClientDisconnectedEvent(clientData.ConnectionId, clientData.AuthKeyId, 0),
            clientData.AuthKeyId);
    }

    private async Task ProcessResponseQueueAsync(ClientData clientData)
    {
        var queue = clientData.EncryptedMessageResponseQueue;
        while (await queue.Reader.WaitToReadAsync() && _isWebSocketConnected)
        {
            while (queue.Reader.TryRead(out var response))
            {
                var encodedBytes =
                    ArrayPool<byte>.Shared.Rent(clientDataSender.GetEncodedDataMaxLength(response.Data.Length));
                try
                {
                    clientManager.UpdateAuthKeyId(clientData, response.AuthKeyId, clientData.ConnectionId);
                    var totalCount = clientDataSender.EncodeData(response, clientData, encodedBytes);
                    await clientData.WebSocket!.SendAsync(encodedBytes.AsMemory()[..totalCount],
                        WebSocketMessageType.Binary, true, default);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(encodedBytes);
                    response.MemoryOwner?.Dispose();
                }
            }
        }
    }

    private async Task ProcessSendQuickAckAsync(ClientData clientData)
    {
        var queue = clientData.QuickAckQueue;
        var encodedBytes = new byte[MtpMessageEncoder.MaxQuickAckLength];
        while (await queue.Reader.WaitToReadAsync() && _isWebSocketConnected)
        {
            while (queue.Reader.TryRead(out var quickAckToken))
            {
                //await clientData.SendLock.WaitAsync();
                //try
                //{
                var totalCount = clientDataSender.EncodeQuickAck(quickAckToken, clientData, encodedBytes);
                if (totalCount > 0)
                {
                    await clientData.WebSocket!.SendAsync(encodedBytes.AsMemory()[..totalCount],
                        WebSocketMessageType.Binary, true, default);
                }
                //}
                //finally
                //{
                //    clientData.SendLock.Release();
                //}
            }
        }
    }

    private async Task ReadPipeAsync(PipeReader reader,
        ClientData clientData)
    {
        try
        {
            while (_isWebSocketConnected)
            {
                var result = await reader.ReadAsync();
                var buffer = result.Buffer;
                if (result.IsCanceled)
                {
                    break;
                }

                try
                {
                    if (!buffer.IsEmpty)
                    {
                        if (!clientData.IsFirstPacketParsed)
                        {
                            messageParser.ProcessFirstUnencryptedPacket(ref buffer, clientData);
                        }

                        while (messageParser.TryParse(ref buffer, clientData, out var mtpMessage))
                        {
                            await ProcessDataAsync(mtpMessage, clientData);
                        }
                    }
                }
                finally
                {
                    reader.AdvanceTo(buffer.Start, buffer.End);
                }

                if (result.IsCompleted)
                {
                    break;
                }
            }
        }
        catch (MtpTransportException ex)
        {
            logger.LogWarning("[ConnectionId: {ConnectionId}] Closing WebSocket because of an MTProto transport error: {Message}",
                clientData.ConnectionId,
                ex.Message);
            // Stops WritePipeAsync (ReceiveAsync fails once the socket is aborted).
            clientData.WebSocket?.Abort();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[ConnectionId: {ConnectionId}] Failed to process WebSocket data, closing connection",
                clientData.ConnectionId);
            clientData.WebSocket?.Abort();
        }
        finally
        {
            await reader.CompleteAsync();
        }
    }

    private async Task WritePipeAsync(WebSocket webSocket,
        PipeWriter writer)
    {
        const int minimumBufferSize = 8192 * 2;
        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var memory = writer.GetMemory(minimumBufferSize);
                var receiveResult = await webSocket.ReceiveAsync(memory, CancellationToken.None);
                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                if (receiveResult.MessageType == WebSocketMessageType.Binary)
                {
                    writer.Advance(receiveResult.Count);
                    var flushResult = await writer.FlushAsync();
                    if (flushResult.IsCompleted || flushResult.IsCanceled)
                    {
                        break;
                    }
                }
            }
        }
        catch (WebSocketException)
        {
            // The client disconnected without a close handshake, or the socket was aborted.
        }
        catch (OperationCanceledException)
        {
            // The socket was aborted.
        }
        finally
        {
            await writer.CompleteAsync();
            _isWebSocketConnected = false;
        }
    }
}
