namespace MyTelegram.Domain.Commands.Channel;

public class SetPinnedMsgIdCommand : DistinctCommand<ChannelAggregate, ChannelId, IExecutionResult>
{
    public SetPinnedMsgIdCommand(ChannelId aggregateId,
        long reqMsgId,
        int pinnedMsgId,
        bool pinned) : base(aggregateId)
    {
        ReqMsgId = reqMsgId;
        PinnedMsgId = pinnedMsgId;
        Pinned = pinned;
    }

    public bool Pinned { get; }
    public int PinnedMsgId { get; }
    public long ReqMsgId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
        yield return BitConverter.GetBytes(PinnedMsgId);
    }
}
