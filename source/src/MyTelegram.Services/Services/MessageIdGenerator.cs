namespace MyTelegram.Services.Services;

public class MessageIdGenerator : IMessageIdGenerator
{
    private long _lastMessageId;
    //private readonly long _timeDelta;

    public Task<long> GenerateServerMessageIdAsync()
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

        //Console.WriteLine($"New server message id:{messageId}");

        return Task.FromResult(messageId);
    }
}