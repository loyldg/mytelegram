namespace MyTelegram.Domain.Aggregates.RpcResult;

public class RpcResultState : AggregateState<RpcResultAggregate, RpcResultId, RpcResultState>,
    IApply<RpcResultCreatedEvent>
{
    public long PeerId { get; private set; }
    public long ReqMsgId { get; private set; }
    public byte[] RpcData { get; private set; } = default!;
    public string SourceId { get; private set; } = default!;

    public void Apply(RpcResultCreatedEvent aggregateEvent)
    {
        ReqMsgId = aggregateEvent.ReqMsgId;
        PeerId = aggregateEvent.PeerId;
        SourceId = aggregateEvent.SourceId;
        RpcData = aggregateEvent.RpcData;
    }
}
