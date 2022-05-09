namespace MyTelegram.Domain.Commands.Dialog;

public class ClearDraftCommand : RequestCommand<DialogAggregate, DialogId, IExecutionResult>
{
    public ClearDraftCommand(DialogId aggregateId,
        long reqMsgId) : base(aggregateId, reqMsgId)
    {
    }
}
