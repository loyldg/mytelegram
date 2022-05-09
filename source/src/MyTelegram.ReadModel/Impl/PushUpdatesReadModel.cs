namespace MyTelegram.ReadModel.Impl;

public class PushUpdatesReadModel : IPushUpdatesReadModel,
    IAmReadModelFor<PushUpdatesAggregate, PushUpdatesId, PushUpdatesCreatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PushUpdatesAggregate, PushUpdatesId, PushUpdatesCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        PeerId = domainEvent.AggregateEvent.ToPeer.PeerId;
        OnlySendToThisAuthKeyId = domainEvent.AggregateEvent.OnlySendToThisAuthKeyId;
        ExcludeAuthKeyId = domainEvent.AggregateEvent.ExcludeAuthKeyId;
        Data = domainEvent.AggregateEvent.Data;
        Pts = domainEvent.AggregateEvent.Pts;
        PtsType = domainEvent.AggregateEvent.PtsType;
        SeqNo = domainEvent.AggregateEvent.SeqNo;

        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;
    public virtual long PeerId { get; private set; }
    public virtual long OnlySendToThisAuthKeyId { get; private set; }
    public virtual long ExcludeAuthKeyId { get; set; }

    public virtual int Pts { get; private set; }
    public virtual PtsType PtsType { get; private set; }
    public virtual long SeqNo { get; private set; }
    public virtual byte[] Data { get; private set; } = null!;
}
