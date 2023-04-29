namespace MyTelegram.GatewayServer.EventHandlers;

public class UnencryptedMessageResponseEventHandler : IEventHandler<UnencryptedMessageResponse>
{
    private readonly IClientDataSender _clientDataSender;

    public UnencryptedMessageResponseEventHandler(
        IClientDataSender clientDataSender)
    {
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(UnencryptedMessageResponse eventData)
    {
        return _clientDataSender.SendAsync(eventData);
    }
}