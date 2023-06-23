namespace MyTelegram.MessengerServer.Services.Impl;

public class MessageIdGenerator : IMessageIdGenerator
{
    private long _lastMessageId;

    public Task<long> GenerateServerMessageIdAsync()
    {
        var unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var messageId = (unixTime / 1000) << 32;
        if (messageId <= _lastMessageId) messageId = _lastMessageId += 4;

        while (messageId % 4 != 1) messageId++;

        _lastMessageId = messageId;

        return Task.FromResult(messageId);
    }
}