namespace MyTelegram.ReadModel.Impl;

public class PtsReadModel : ReadModelBase, IPtsReadModel,
    IAmReadModelFor<PtsAggregate, PtsId, PtsUpdatedEvent>,
    IAmReadModelFor<PtsAggregate, PtsId, QtsUpdatedEvent>,
    IAmReadModelFor<PtsAggregate, PtsId, PtsGlobalSeqNoUpdatedEvent>
{
    public virtual int Date { get; private set; }
    public virtual long GlobalSeqNo { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual long PeerId { get; private set; }
    public virtual int Pts { get; private set; }
    public virtual int Qts { get; private set; }
    public virtual int UnreadCount { get; private set; }
    public virtual long? Version { get; set; }

    public virtual int MaxMessageId { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PtsAggregate, PtsId, PtsGlobalSeqNoUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = PtsId.Create(domainEvent.AggregateEvent.PeerId).Value;
        GlobalSeqNo = domainEvent.AggregateEvent.GlobalSeqNo;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<PtsAggregate, PtsId, PtsUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (Pts >= domainEvent.AggregateEvent.NewPts)
        {
            return Task.CompletedTask;
        }

        Id = PtsId.Create(domainEvent.AggregateEvent.PeerId).Value;
        PeerId = domainEvent.AggregateEvent.PeerId;
        Pts = domainEvent.AggregateEvent.NewPts;
        Date = domainEvent.AggregateEvent.Date;

        UnreadCount += domainEvent.AggregateEvent.ChangedUnreadCount;
        if (domainEvent.AggregateEvent.MessageId.HasValue && domainEvent.AggregateEvent.MessageId > MaxMessageId)
        {
            MaxMessageId = domainEvent.AggregateEvent.MessageId.Value;
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<PtsAggregate, PtsId, QtsUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (Qts >= domainEvent.AggregateEvent.NewQts)
        {
            return Task.CompletedTask;
        }

        Id = PtsId.Create(domainEvent.AggregateEvent.PeerId).Value;
        PeerId = domainEvent.AggregateEvent.PeerId;
        Qts = domainEvent.AggregateEvent.NewQts;
        Date = domainEvent.AggregateEvent.Date;


        return Task.CompletedTask;
    }
}