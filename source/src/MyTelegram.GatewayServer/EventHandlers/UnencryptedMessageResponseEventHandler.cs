namespace MyTelegram.GatewayServer.EventHandlers;

public class UnencryptedMessageResponseEventHandler : IEventHandler<MyTelegram.Core.UnencryptedMessageResponse>
{
    private readonly IClientDataSender _clientDataSender;

    public UnencryptedMessageResponseEventHandler(
        IClientDataSender clientDataSender)
    {
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(MyTelegram.Core.UnencryptedMessageResponse eventData)
    {
        return _clientDataSender.SendAsync(new MyTelegram.MTProto.UnencryptedMessageResponse(eventData.AuthKeyId,eventData.Data,eventData.ConnectionId,eventData.ReqMsgId));
    }
}
