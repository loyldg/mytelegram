namespace MyTelegram.GatewayServer.EventHandlers;

public class EncryptedMessageResponseEventHandler : IEventHandler<EncryptedMessageResponse>
{
    private readonly IClientDataSender _clientDataSender;

    public EncryptedMessageResponseEventHandler(
        IClientDataSender clientDataSender)
    {
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(EncryptedMessageResponse eventData)
    {
        return _clientDataSender.SendAsync(eventData);
    }
}
