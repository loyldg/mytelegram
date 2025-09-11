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
                    ConnectionType = connectionTypeFeature?.ConnectionType ?? ConnectionType.Generic
                };
                clientManager.AddClient(context.Connection.Id, clientData);

                await ProcessWebSocketAsync(webSocket, clientData);
            }
            else
            {
                Console.WriteLine($"Not allowed path:{context.Request.Path}");
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
                        logger.LogWarning(
                            "[0] Cannot find cached client info, skip sending message, connectionId: {ConnectionId}",
                            clientData.ConnectionId);
                        continue;
                    }
                    
                    var encodedBytes = ArrayPool<byte>.Shared.Rent(clientDataSender.GetEncodedDataMaxLength(response.Data.Length));
                    try
                    {
                        var totalCount = clientDataSender.EncodeData(response, d, encodedBytes);
                        //await SendAsync(encodedBytes.AsMemory()[..totalCount], connectionContext);
                        await clientData.WebSocket!.SendAsync(encodedBytes.AsMemory()[..totalCount],
                            WebSocketMessageType.Binary, true, default);
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
        if (clientData.IsFirstPacketParsed)
        {
            mtpMessage.ConnectionId = clientData.ConnectionId;
            mtpMessage.ClientIp = clientData.ClientIp;
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

        await Task.WhenAll(processSendUnencryptedDataTask, writeTask, readTask, processResponseQueueTask);
        clientManager.RemoveClient(clientData.ConnectionId);
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
                }
            }
        }
    }

    private async Task ReadPipeAsync(PipeReader reader,
        ClientData clientData)
    {
        while (_isWebSocketConnected)
        {
            var result = await reader.ReadAsync();
            var buffer = result.Buffer;
            if (result.IsCanceled)
            {
                break;
            }

            if (result.Buffer.IsEmpty)
            {
                continue;
            }

            if (!clientData.IsFirstPacketParsed)
            {
                messageParser.ProcessFirstUnencryptedPacket(ref buffer, clientData);
            }

            while (TryParseMessage(ref buffer, clientData, out var mtpMessage))
            {
                await ProcessDataAsync(mtpMessage, clientData);
            }

            reader.AdvanceTo(buffer.Start, buffer.End);

            if (result.IsCompleted)
            {
                break;
            }
        }

        await reader.CompleteAsync();
    }

    private bool TryParseMessage(ref ReadOnlySequence<byte> buffer,
        ClientData clientData,
        [NotNullWhen(true)] out IMtpMessage? mtpMessage)
    {
        if (buffer.Length == 0)
        {
            mtpMessage = default;
            return false;
        }

        var reader = new SequenceReader<byte>(buffer);

        if (reader.Remaining < 4)
        {
            mtpMessage = default;

            return false;
        }

        return messageParser.TryParse(ref buffer, clientData, out mtpMessage);
    }

    private async Task WritePipeAsync(WebSocket webSocket,
        PipeWriter writer)
    {
        const int minimumBufferSize = 8192 * 2;
        while (webSocket.State == WebSocketState.Open)
        {
            var memory = writer.GetMemory(minimumBufferSize);
            var receiveResult = await webSocket.ReceiveAsync(memory, CancellationToken.None);
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

        await writer.CompleteAsync();
        _isWebSocketConnected = false;
    }
}
