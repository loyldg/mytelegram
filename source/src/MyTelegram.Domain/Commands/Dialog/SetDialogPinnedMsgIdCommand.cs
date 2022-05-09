namespace MyTelegram.Domain.Commands.Dialog;

public class SetDialogPinnedMsgIdCommand : DistinctCommand<DialogAggregate, DialogId, IExecutionResult>
{
    public SetDialogPinnedMsgIdCommand(DialogId aggregateId,
        long reqMsgId,
        int pinnedMsgId) : base(aggregateId)
    {
        ReqMsgId = reqMsgId;
        PinnedMsgId = pinnedMsgId;
    }

    public int PinnedMsgId { get; }

    public long ReqMsgId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
        yield return BitConverter.GetBytes(PinnedMsgId);
    }
}
