namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageIdAddedToOutboxMessageEvent : AggregateEvent<MessageAggregate, MessageId>
{
    public InboxItem InboxItem { get; }

    public InboxMessageIdAddedToOutboxMessageEvent(InboxItem inboxItem)
    {
        InboxItem = inboxItem;
    }
}