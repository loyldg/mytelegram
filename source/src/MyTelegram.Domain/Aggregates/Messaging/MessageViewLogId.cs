namespace MyTelegram.Domain.Aggregates.Messaging;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<MessageViewLogId>))]
public class MessageViewLogId : MyIdentity<MessageViewLogId>
{
    public MessageViewLogId(string value) : base(value)
    {
    }

    public static MessageViewLogId Create(long channelId,
        long userId,
        int messageId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"messageviewlog_{channelId}_{userId}_{messageId}");
    }
}
