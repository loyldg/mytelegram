namespace MyTelegram.Domain.Commands.Dialog;

public class
    ClearChannelHistoryCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult> //, IHasCorrelationId
{
    public ClearChannelHistoryCommand(DialogId aggregateId,
        RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }

    //public Guid CorrelationId { get; }
}
