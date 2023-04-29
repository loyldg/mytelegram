namespace MyTelegram.MTProto;

public interface IMtpMessageDispatcher
{
    Task DispatchAsync(IMtpMessage message);
}
