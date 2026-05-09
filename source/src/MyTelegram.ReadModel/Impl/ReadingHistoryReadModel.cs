namespace MyTelegram.ReadModel.Impl;

public class ReadingHistoryReadModel : ReadModelBase, IReadingHistoryReadModel,
    IAmReadModelFor<ReadingHistoryAggregate, ReadingHistoryId, ReadingHistoryCreatedEvent>//,
    //IAmReadModelFor<DialogAggregate,DialogId, ReadInboxMessage2Event>
{
    public int Date { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual long MessageId { get; private set; }
    public virtual long ReaderPeerId { get; private set; }
    public virtual long TargetPeerId { get; private set; }
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
        Date = domainEvent.AggregateEvent.Date;

        return Task.CompletedTask;
    }
    //public Task ApplyAsync(IReadModelContext context, IDomainEvent<DialogAggregate, DialogId, ReadInboxMessage2Event> domainEvent, CancellationToken cancellationToken)
    //{
    //    Id = ReadingHistoryId.Create(domainEvent.AggregateEvent.ReaderUserId, domainEvent.AggregateEvent.ToPeer.PeerId,
    //        domainEvent.AggregateEvent.MaxMessageId).Value;
    //    ReaderPeerId = domainEvent.AggregateEvent.ReaderUserId;
    //    TargetPeerId=domainEvent.AggregateEvent.ToPeer.PeerId;
    //    MessageId=domainEvent.AggregateEvent.MaxMessageId;
    //    Date = domainEvent.AggregateEvent.Date;

    //    return Task.CompletedTask;
    //}
}
