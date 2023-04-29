namespace MyTelegram.MTProto;

public interface IMessageIdHelper
{
    long GenerateMessageId();

    long GenerateUniqueId();
}
