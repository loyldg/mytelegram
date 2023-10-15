namespace MyTelegram.GatewayServer.EventHandlers;

public interface IClientDataSender
{
    Task SendAsync(ReadOnlyMemory<byte> data,
        ClientData clientData);

    Task SendAsync(MyTelegram.MTProto.UnencryptedMessageResponse data);
    Task SendAsync(MyTelegram.MTProto.EncryptedMessageResponse data);
}
