namespace MyTelegram.Domain.Aggregates.Messaging;

public class ReadingHistoryState : AggregateState<ReadingHistoryAggregate, ReadingHistoryId, ReadingHistoryState>,
    IApply<ReadingHistoryCreatedEvent>
{
    public long ReaderPeerId { get; private set; }
    public long TargetPeerId { get; private set; }
    public int MessageId { get; private set; }
    public void Apply(ReadingHistoryCreatedEvent aggregateEvent)
    {
        ReaderPeerId = aggregateEvent.ReaderPeerId;
        TargetPeerId = aggregateEvent.TargetPeerId;
        MessageId = aggregateEvent.MessageId;
    }
}