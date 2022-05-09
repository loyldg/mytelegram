namespace MyTelegram.ReadModel.Impl;

public class PtsForAuthKeyIdReadModel : IPtsForAuthKeyIdReadModel,
    IAmReadModelFor<PtsAggregate, PtsId, PtsAckedEvent>,
    IAmReadModelFor<PtsAggregate, PtsId, PtsUpdatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PtsAggregate, PtsId, PtsAckedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        PeerId = domainEvent.AggregateEvent.PeerId;
        PermAuthKeyId = domainEvent.AggregateEvent.PermAuthKeyId;

        //if (domainEvent.AggregateEvent.ToPeer.PeerType != PeerType.Channel)
        //{
        //    Pts = domainEvent.AggregateEvent.Pts;
        //}

        if (domainEvent.AggregateEvent.ToPeer.PeerType != PeerType.Channel)
        {
            if (domainEvent.AggregateEvent.Pts != 0)
            {
                Pts = domainEvent.AggregateEvent.Pts;
            }
        }

        if (domainEvent.AggregateEvent.GlobalSeqNo != 0)
        {
            GlobalSeqNo = domainEvent.AggregateEvent.GlobalSeqNo;
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PtsAggregate, PtsId, PtsUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        PeerId = domainEvent.AggregateEvent.PeerId;
        PermAuthKeyId = domainEvent.AggregateEvent.PermAuthKeyId;
        Pts = domainEvent.AggregateEvent.NewPts;

        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;
    public virtual long PeerId { get; private set; }
    public virtual long PermAuthKeyId { get; private set; }
    public virtual int Pts { get; private set; }
    public virtual long GlobalSeqNo { get; private set; }
}