namespace MyTelegram.ReadModel.Impl;

public class RpcResultReadModel : IRpcResultReadModel,
    IAmReadModelFor<RpcResultAggregate, RpcResultId, RpcResultCreatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<RpcResultAggregate, RpcResultId, RpcResultCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ReqMsgId = domainEvent.AggregateEvent.ReqMsgId;
        PeerId = domainEvent.AggregateEvent.PeerId;
        SourceId = domainEvent.AggregateEvent.SourceId;
        RpcData = domainEvent.AggregateEvent.RpcData;
        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;
    public virtual long ReqMsgId { get; private set; }
    public virtual long PeerId { get; private set; }
    public virtual string SourceId { get; private set; } = null!;
    public virtual byte[] RpcData { get; private set; } = null!;
}
