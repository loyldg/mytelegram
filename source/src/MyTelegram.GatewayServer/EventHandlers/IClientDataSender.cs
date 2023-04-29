namespace MyTelegram.GatewayServer.EventHandlers;

public interface IClientDataSender
{
    Task SendAsync(ReadOnlyMemory<byte> data,
        ClientData clientData);

    Task SendAsync(UnencryptedMessageResponse data);
    Task SendAsync(EncryptedMessageResponse data);
}