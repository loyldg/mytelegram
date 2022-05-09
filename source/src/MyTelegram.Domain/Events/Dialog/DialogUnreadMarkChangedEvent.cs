namespace MyTelegram.Domain.Events.Dialog;

public class DialogUnreadMarkChangedEvent : AggregateEvent<DialogAggregate, DialogId>
{
    public DialogUnreadMarkChangedEvent(bool unreadMark)
    {
        UnreadMark = unreadMark;
    }

    public bool UnreadMark { get; }
}
