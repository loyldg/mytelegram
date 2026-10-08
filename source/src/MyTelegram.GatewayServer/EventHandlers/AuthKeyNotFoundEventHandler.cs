namespace MyTelegram.GatewayServer.EventHandlers;

public class AuthKeyNotFoundEventHandler(IClientDataSender clientDataSender)
    : IEventHandler<AuthKeyNotFoundEvent>, ITransientDependency
{
    // 0x6c, 0xfe, 0xff, 0xff
    private static readonly byte[] AuthKeyNotFoundData = [0x6c, 0xfe, 0xff, 0xff]; //-404

    public async Task HandleEventAsync(AuthKeyNotFoundEvent eventData)
    {
        var m = new EncryptedMessageResponse(eventData.AuthKeyId, AuthKeyNotFoundData, eventData.ConnectionId, 2);
        await clientDataSender.SendAsync(m);
    }
}