namespace MyTelegram.GatewayServer;

public class WebsocketMiddleware : IMiddleware
{
    private readonly IClientManager _clientManager;
    private readonly IMtpMessageDispatcher _messageDispatcher;
    private readonly IMtpMessageParser _messageParser;
    private readonly string _subProtocol = "binary";

    public WebsocketMiddleware(IMtpMessageParser messageParser,
        IMtpMessageDispatcher messageDispatcher,
        IClientManager clientManager)
    {
        _messageParser = messageParser;
        _messageDispatcher = messageDispatcher;
        _clientManager = clientManager;
    }

    public async Task InvokeAsync(HttpContext context,
        RequestDelegate next)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            if (context.Request.Path == "/apiws")
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync(_subProtocol);
                var clientData = new ClientData
                {
                    ConnectionId = context.Connection.Id,
                    WebSocket = webSocket,
                    ClientType = ClientType.WebSocket,
                    ClientIp = context.Connection.RemoteIpAddress?.ToString()
                };
                _clientManager.AddClient(context.Connection.Id, clientData);

                await ProcessWebSocketAsync(webSocket, clientData);
            }
        }
        else
        {
            await next(context);
        }
    }

    private Task ProcessDataAsync(IMtpMessage mtpMessage,
        ClientData clientData)
    {
        if (clientData.IsFirstPacketParsed)
        {
            mtpMessage.ConnectionId = clientData.ConnectionId;
            mtpMessage.ClientIp = clientData.ClientIp;
            return _messageDispatcher.DispatchAsync(mtpMessage);
        }

        return Task.CompletedTask;
    }

    private async Task ProcessWebSocketAsync(WebSocket webSocket,
        ClientData clientData)
    {
        var pipe = new Pipe();
        var writeTask = WritePipeAsync(webSocket, pipe.Writer);
        var readTask = ReadPipeAsync(pipe.Reader, clientData);
        await Task.WhenAll(writeTask, readTask);
    }

    private async Task ReadPipeAsync(PipeReader reader,
        ClientData clientData)
    {
        while (true)
        {
            var result = await reader.ReadAsync();
            var buffer = result.Buffer;
            if (result.IsCanceled)
            {
                break;
            }

            if (!clientData.IsFirstPacketParsed)
            {
                _messageParser.ProcessFirstUnencryptedPacket(ref buffer, clientData);
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

        return _messageParser.TryParse(ref buffer, clientData, out mtpMessage);
    }

    private async Task WritePipeAsync(WebSocket webSocket,
        PipeWriter writer)
    {
        const int minimumBufferSize = 8192;
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
    }
}
