namespace MyTelegram.ReadModel.Impl;

public class ReadingHistoryReadModel : IReadingHistoryReadModel,
    IAmReadModelFor<ReadingHistoryAggregate, ReadingHistoryId, ReadingHistoryCreatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ReadingHistoryAggregate, ReadingHistoryId, ReadingHistoryCreatedEvent>
            domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ReaderPeerId = domainEvent.AggregateEvent.ReaderPeerId;
        //PeerType = domainEvent.AggregateEvent.PeerType;
        TargetPeerId = domainEvent.AggregateEvent.TargetPeerId;
        MessageId = domainEvent.AggregateEvent.MessageId;

        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;
    public virtual long ReaderPeerId { get; private set; }
    public int Date { get; }
    public virtual long TargetPeerId { get; private set; }
    public virtual long MessageId { get; private set; }
}
