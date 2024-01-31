namespace MyTelegram.GatewayServer.EventHandlers;

public interface IClientDataSender
{
    Task SendAsync(ReadOnlyMemory<byte> data,
        ClientData clientData);

    Task SendAsync(MTProto.UnencryptedMessageResponse data);
    Task SendAsync(MTProto.EncryptedMessageResponse data);
}
