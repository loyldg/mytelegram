namespace MyTelegram.Domain.Aggregates.Messaging;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<MessageId>))]
public class MessageId : MyIdentity<MessageId>
{
    public MessageId(string value) : base(value)
    {
    }

    public static MessageId Create(long ownerPeerId,
        int messageId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"message_{ownerPeerId}_{messageId}");
    }

    public static MessageId CreateWithRandomId(long ownerPeerId,
        long randomId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"message_{ownerPeerId}_randomId_{randomId}");
    }
}
