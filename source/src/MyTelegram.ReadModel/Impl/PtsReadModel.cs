namespace MyTelegram.ReadModel.Impl;

public class PtsReadModel : IPtsReadModel,
    IAmReadModelFor<PushUpdatesAggregate, PushUpdatesId, PushUpdatesCreatedEvent>,
    IAmReadModelFor<PushUpdatesAggregate, PushUpdatesId, EncryptedPushUpdatesCreatedEvent>,
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

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PtsAggregate, PtsId, PtsGlobalSeqNoUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        GlobalSeqNo = domainEvent.AggregateEvent.GlobalSeqNo;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PushUpdatesAggregate, PushUpdatesId, EncryptedPushUpdatesCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = PtsId.Create(domainEvent.AggregateEvent.InboxOwnerPeerId).Value;
        }

        PeerId = domainEvent.AggregateEvent.InboxOwnerPeerId;
        Qts = domainEvent.AggregateEvent.Qts;
        //Date = DateTime.UtcNow.ToTimestamp();
        Date = domainEvent.AggregateEvent.Date;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PushUpdatesAggregate, PushUpdatesId, PushUpdatesCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = PtsId.Create(domainEvent.AggregateEvent.ToPeer.PeerId).Value;
        }

        PeerId = domainEvent.AggregateEvent.ToPeer.PeerId;

        if (domainEvent.AggregateEvent.ToPeer.PeerType != PeerType.Channel)
        {
            Pts = domainEvent.AggregateEvent.Pts;
        }

        GlobalSeqNo = domainEvent.AggregateEvent.SeqNo;

        //Date = DateTime.UtcNow.ToTimestamp();
        Date = domainEvent.AggregateEvent.Date;
        UnreadCount = 0;

        //if (domainEvent.AggregateEvent.PtsType == PtsType.NewMessages)
        //{
        //    UnreadCount++;
        //}

        return Task.CompletedTask;
    }
}
