namespace MyTelegram.ReadModel.Impl;

public class PtsForAuthKeyIdReadModel : ReadModelBase, IPtsForAuthKeyIdReadModel,
    IAmReadModelFor<PtsAggregate, PtsId, PtsForAuthKeyIdUpdatedEvent>
{
    public virtual long GlobalSeqNo { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual long PeerId { get; private set; }
    public virtual long PermAuthKeyId { get; private set; }
    public virtual int Pts { get; private set; }
    //public int UnreadCount { get; private set; }
    public int Qts { get; private set; }

    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PtsAggregate, PtsId, PtsForAuthKeyIdUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        PeerId = domainEvent.AggregateEvent.PeerId;
        PermAuthKeyId = domainEvent.AggregateEvent.PermAuthKeyId;

        if (domainEvent.AggregateEvent.GlobalSeqNo != 0)
        {
            GlobalSeqNo = domainEvent.AggregateEvent.GlobalSeqNo;
        }

        if (domainEvent.AggregateEvent.Pts != 0)
        {
            Pts = domainEvent.AggregateEvent.Pts;
        }

        return Task.CompletedTask;
    }
}