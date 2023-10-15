namespace MyTelegram.Domain.Commands.Dialog;

public class ToggleDialogPinnedCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public ToggleDialogPinnedCommand(DialogId aggregateId,
        RequestInfo requestInfo,
        bool pinned) : base(aggregateId, requestInfo)
    {
        Pinned = pinned;
    }

    public bool Pinned { get; }
}