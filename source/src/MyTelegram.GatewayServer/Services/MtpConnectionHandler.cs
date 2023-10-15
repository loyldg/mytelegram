namespace MyTelegram.GatewayServer.Services;

public class MtpConnectionHandler : ConnectionHandler
{
    private readonly IClientManager _clientManager;
    private readonly ILogger<MtpConnectionHandler> _logger;
    private readonly IMtpMessageDispatcher _messageDispatcher;
    private readonly IMtpMessageParser _messageParser;
    private readonly IEventBus _eventBus;
    private readonly IMessageQueueProcessor<ClientDisconnectedEvent> _messageQueueProcessor;

    public MtpConnectionHandler(IClientManager clientManager,
        IMtpMessageParser messageParser,
        IMtpMessageDispatcher messageDispatcher,
        ILogger<MtpConnectionHandler> logger, IEventBus eventBus, IMessageQueueProcessor<ClientDisconnectedEvent> messageQueueProcessor)
    {
        _clientManager = clientManager;
        _messageParser = messageParser;
        _messageDispatcher = messageDispatcher;
        _logger = logger;
        _eventBus = eventBus;
        _messageQueueProcessor = messageQueueProcessor;
    }

    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        _logger.LogInformation("[ConnectionId={ConnectionId}] New client connected,RemoteEndPoint:{RemoteEndPoint},online count:{OnlineCount}",
            connection.ConnectionId,
            connection.RemoteEndPoint, _clientManager.GetOnlineCount());
        _clientManager.AddClient(connection.ConnectionId,
            new ClientData
            {
                ConnectionContext = connection,
                ConnectionId = connection.ConnectionId,
                ClientType = ClientType.Tcp
            });
        connection.ConnectionClosed.Register(() =>
        {
            if (_clientManager.TryRemoveClient(connection.ConnectionId, out var clientData))
            {
                //_eventBus.PublishAsync(new ClientDisconnectedEvent(connection.ConnectionId, clientData.AuthKeyId, 0));
                _messageQueueProcessor.Enqueue(new ClientDisconnectedEvent(clientData.ConnectionId, clientData.AuthKeyId, 0), clientData.AuthKeyId);

            }
            _logger.LogInformation("[ConnectionId={ConnectionId}] Client disconnected,RemoteEndPoint:{RemoteEndPoint}",
                connection.ConnectionId,
                connection.RemoteEndPoint);
        });

        var input = connection.Transport.Input;
        while (!connection.ConnectionClosed.IsCancellationRequested)
        {
            var result = await input.ReadAsync();
            if (result.IsCanceled)
            {
                break;
            }

            var buffer = result.Buffer;
            //_logger.LogInformation("[ConnectionId={ConnectionId}] Receive data:{Length}", connection.ConnectionId, buffer.Length);
            //while (TryReadData(connection.ConnectionId, ref buffer, out var data, out var clientData/*, out consumed*/))
            //{
            //    await ProcessDataAsync(data, clientData);
            //}
            if (!_clientManager.TryGetClientData(connection.ConnectionId, out var clientData))
            {
                _logger.LogWarning("Can not find client data,connectionId={ConnectionId}", connection.ConnectionId);
                break;
                //throw new InvalidOperationException($"Can not find client data,connectionId={connection.ConnectionId}");
            }

            if (!clientData.IsFirstPacketParsed)
            {
                _messageParser.ProcessFirstUnencryptedPacket(ref buffer, clientData);
            }

            while (TryParseMessage(ref buffer, clientData, out var mtpMessage))
            {
                await ProcessDataAsync(mtpMessage, clientData);
            }

            input.AdvanceTo(buffer.Start, buffer.End);
            if (result.IsCompleted)
            {
                break;
            }
        }
    }

    private Task ProcessDataAsync(IMtpMessage mtpMessage,
        ClientData clientData)
    {
        if (clientData.IsFirstPacketParsed)
        {
            mtpMessage.ConnectionId = clientData.ConnectionId;
            mtpMessage.ClientIp = (clientData.ConnectionContext!.RemoteEndPoint as IPEndPoint)?.Address.ToString() ?? string.Empty;
            return _messageDispatcher.DispatchAsync(mtpMessage);
        }

        return Task.CompletedTask;
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
}
