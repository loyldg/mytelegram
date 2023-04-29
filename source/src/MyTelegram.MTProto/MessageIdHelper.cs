namespace MyTelegram.MTProto;

public class MessageIdHelper : IMessageIdHelper
{
    private long _lastMessageId;
    //private readonly long _timeDelta = 0;

    public long GenerateMessageId()
    {
        var unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var messageId = (unixTime / 1000) << 32;
        if (messageId <= _lastMessageId)
        {
            messageId = _lastMessageId += 4;
        }

        while (messageId % 4 != 1)
        {
            messageId++;
        }

        _lastMessageId = messageId;

        return messageId;
    }

    public long GenerateUniqueId()
    {
        return BitConverter.ToInt64(Guid.NewGuid().ToByteArray());
    }
}
