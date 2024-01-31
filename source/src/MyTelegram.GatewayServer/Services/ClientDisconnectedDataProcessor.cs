namespace MyTelegram.GatewayServer.Services;

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