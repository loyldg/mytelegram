namespace MyTelegram.Domain.Commands.RpcResult;

public class CreateRpcResultCommand : RequestCommand<RpcResultAggregate, RpcResultId, IExecutionResult>
{
    public CreateRpcResultCommand(RpcResultId aggregateId,
        long reqMsgId,
        long peerId,
        string sourceId,
        byte[] rpcData) : base(aggregateId, reqMsgId)
    {
        PeerId = peerId;
        SourceId = sourceId;
        RpcData = rpcData;
    }

    public long PeerId { get; }

    public byte[] RpcData { get; }
    public new string SourceId { get; }
}
