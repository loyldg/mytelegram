namespace MyTelegram.Domain.Events.Dialog;

public class DialogMsgIdPinnedEvent : AggregateEvent<DialogAggregate, DialogId>
{
    public DialogMsgIdPinnedEvent(int pinnedMsgId)
    {
        PinnedMsgId = pinnedMsgId;
    }

    public int PinnedMsgId { get; }
}
