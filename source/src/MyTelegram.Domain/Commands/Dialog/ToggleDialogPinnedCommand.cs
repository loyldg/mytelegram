namespace MyTelegram.Domain.Commands.Dialog;

public class ToggleDialogPinnedCommand : RequestCommand<DialogAggregate, DialogId, IExecutionResult>
{
    public ToggleDialogPinnedCommand(DialogId aggregateId,
        long reqMsgId,
        bool pinned) : base(aggregateId, reqMsgId)
    {
        Pinned = pinned;
    }

    public bool Pinned { get; }
}
