namespace MyTelegram.GatewayServer.EventHandlers;

public class AuthKeyNotFoundEventHandler : IEventHandler<AuthKeyNotFoundEvent>
{
    private static readonly byte[] AuthKeyNotFoundData = { 0x6c, 0xfe, 0xff, 0xff }; //-404
    private readonly IClientDataSender _clientDataSender;
    private readonly IClientManager _clientManager;

    public AuthKeyNotFoundEventHandler(IClientManager clientManager,
        IClientDataSender clientDataSender)
    {
        _clientManager = clientManager;
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(AuthKeyNotFoundEvent eventData)
    {
        if (_clientManager.TryGetClientData(eventData.ConnectionId, out var d))
        {
            return _clientDataSender.SendAsync(AuthKeyNotFoundData, d);
        }

        return Task.CompletedTask;
    }
}
