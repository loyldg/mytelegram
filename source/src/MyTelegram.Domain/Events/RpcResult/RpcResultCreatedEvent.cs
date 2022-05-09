namespace MyTelegram.Domain.Events.RpcResult;

public class RpcResultCreatedEvent : AggregateEvent<RpcResultAggregate, RpcResultId>
{
    public RpcResultCreatedEvent(long reqMsgId,
        long peerId,
        string sourceId,
        byte[] rpcData)
    {
        ReqMsgId = reqMsgId;
        PeerId = peerId;
        SourceId = sourceId;
        RpcData = rpcData;
    }

    public long PeerId { get; }
    public long ReqMsgId { get; }
    public byte[] RpcData { get; }
    public string SourceId { get; }
}
