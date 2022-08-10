namespace MyTelegram.Domain.Commands.Dialog;

public class UpdateDialogFilterCommand : RequestCommand2<DialogFilterAggregate, DialogFilterId, IExecutionResult>
{
    public long OwnerUserId { get; }
    public DialogFilter Filter { get; }

    public UpdateDialogFilterCommand(DialogFilterId aggregateId, RequestInfo request, long ownerUserId, DialogFilter filter) : base(aggregateId, request)
    {
        OwnerUserId = ownerUserId;
        Filter = filter;
    }
}