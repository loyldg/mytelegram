namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageIdAddedToOutboxMessageEvent : AggregateEvent<MessageAggregate, MessageId>
{
    public InboxMessageIdAddedToOutboxMessageEvent(InboxItem inboxItem)
    {
        InboxItem = inboxItem;
    }

    public InboxItem InboxItem { get; }
}