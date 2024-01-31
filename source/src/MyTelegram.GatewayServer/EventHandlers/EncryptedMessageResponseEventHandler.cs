namespace MyTelegram.GatewayServer.EventHandlers;

public class EncryptedMessageResponseEventHandler : IEventHandler<Core.EncryptedMessageResponse>
{
    private readonly IClientDataSender _clientDataSender;

    public EncryptedMessageResponseEventHandler(
        IClientDataSender clientDataSender)
    {
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(Core.EncryptedMessageResponse eventData)
    {
        return _clientDataSender.SendAsync(new MTProto.EncryptedMessageResponse(eventData.AuthKeyId,eventData.Data,eventData.ConnectionId,eventData.SeqNumber));
    }
}
