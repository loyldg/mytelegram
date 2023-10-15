namespace MyTelegram.GatewayServer.Services;

public interface IClientManager
{
    void AddClient(string connectionId,
        ClientData clientData);

    void RemoveClient(string connectionId);

    bool TryGetClientData(string connectionId,
        [NotNullWhen(true)] out ClientData? clientData);

    bool TryRemoveClient(string connectionId,[NotNullWhen(true)]out ClientData? clientData);

    int GetOnlineCount();
}

public class ClientDisconnectedDataProcessor:IDataProcessor<ClientDisconnectedEvent>
{
    private readonly IEventBus _eventBus;

    public ClientDisconnectedDataProcessor(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task ProcessAsync(ClientDisconnectedEvent data)
    {
        return _eventBus.PublishAsync(data);
    }
}
