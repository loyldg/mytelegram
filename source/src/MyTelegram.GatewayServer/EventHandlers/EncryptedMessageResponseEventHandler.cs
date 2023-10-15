namespace MyTelegram.GatewayServer.EventHandlers;

public class EncryptedMessageResponseEventHandler : IEventHandler<MyTelegram.Core.EncryptedMessageResponse>
{
    private readonly IClientDataSender _clientDataSender;

    public EncryptedMessageResponseEventHandler(
        IClientDataSender clientDataSender)
    {
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(MyTelegram.Core.EncryptedMessageResponse eventData)
    {
        return _clientDataSender.SendAsync(new MyTelegram.MTProto.EncryptedMessageResponse(eventData.AuthKeyId,eventData.Data,eventData.ConnectionId,eventData.SeqNumber));
    }
}
