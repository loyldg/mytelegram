namespace MyTelegram.Domain.Aggregates.RpcResult;

public class RpcResultAggregate : AggregateRoot<RpcResultAggregate, RpcResultId>
{
    private readonly RpcResultState _state = new();

    public RpcResultAggregate(RpcResultId id) : base(id)
    {
        Register(_state);
    }

    public void Create(long reqMsgId,
        long peerId,
        string sourceId,
        byte[] rpcData)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new RpcResultCreatedEvent(reqMsgId, peerId, sourceId, rpcData));
    }
}
