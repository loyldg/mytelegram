namespace MyTelegram.Domain.Aggregates.Messaging;

public class ReadingHistoryAggregate : AggregateRoot<ReadingHistoryAggregate, ReadingHistoryId>
{
    private readonly ReadingHistoryState _state = new();
    public ReadingHistoryAggregate(ReadingHistoryId id) : base(id)
    {
        Register(_state);
    }

    public void Create(
        long readerPeerId,
        long targetPeerId,
        int messageId,
        int date
        )
    {
        if (IsNew)
        {
            Emit(new ReadingHistoryCreatedEvent(readerPeerId, targetPeerId, messageId, date));
        }
    }
}