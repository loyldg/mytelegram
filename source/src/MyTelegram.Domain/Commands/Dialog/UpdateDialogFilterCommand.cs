namespace MyTelegram.Domain.Commands.Dialog;

public class UpdateDialogFilterCommand : RequestCommand2<DialogFilterAggregate, DialogFilterId, IExecutionResult>
{
    public long OwnerUserId { get; }
    public DialogFilter Filter { get; }

    public UpdateDialogFilterCommand(DialogFilterId aggregateId, RequestInfo requestInfo, long ownerUserId, DialogFilter filter) : base(aggregateId, requestInfo)
    {
        OwnerUserId = ownerUserId;
        Filter = filter;
    }
}