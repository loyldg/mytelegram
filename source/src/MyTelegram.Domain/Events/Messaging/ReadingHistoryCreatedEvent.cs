namespace MyTelegram.Domain.Events.Messaging;

public class ReadingHistoryCreatedEvent : AggregateEvent<ReadingHistoryAggregate, ReadingHistoryId>
{
    public ReadingHistoryCreatedEvent(long readerPeerId,
        long targetPeerId,
        int messageId,
        int date)
    {
        ReaderPeerId = readerPeerId;
        TargetPeerId = targetPeerId;
        MessageId = messageId;
        Date = date;
    }

    public long ReaderPeerId { get; }
    public long TargetPeerId { get; }
    public int MessageId { get; }
    public int Date { get; }
}
