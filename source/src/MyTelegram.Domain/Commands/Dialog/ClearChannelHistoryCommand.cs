namespace MyTelegram.Domain.Commands.Dialog;

public class
    ClearChannelHistoryCommand : RequestCommand<DialogAggregate, DialogId, IExecutionResult> //, IHasCorrelationId
{
    public ClearChannelHistoryCommand(DialogId aggregateId,
        long reqMsgId) : base(aggregateId, reqMsgId)
    {
    }

    //public Guid CorrelationId { get; }
}
