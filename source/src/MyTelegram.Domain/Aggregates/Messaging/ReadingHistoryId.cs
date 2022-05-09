namespace MyTelegram.Domain.Aggregates.Messaging;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ReadingHistoryId>))]
public class ReadingHistoryId : MyIdentity<ReadingHistoryId>
{
    public ReadingHistoryId(string value) : base(value)
    {
    }

    public static ReadingHistoryId Create(long readerPeerId,
        long targetPeerId,
        int messageId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"ReadingHistoryId_{readerPeerId}_{targetPeerId}_{messageId}");
    }
}