namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageIdAddedToOutboxMessageEvent : AggregateEvent<MessageAggregate, MessageId>
{
    public InboxMessageIdAddedToOutboxMessageEvent(InboxItem inboxItem)
    {
        InboxItem = inboxItem;
    }

    public InboxItem InboxItem { get; }
}

public class InboxItemsAddedToOutboxMessageEvent : AggregateEvent<MessageAggregate, MessageId>
{
    public List<InboxItem> InboxItems { get; }

    public InboxItemsAddedToOutboxMessageEvent(List<InboxItem> inboxItems)
    {
        InboxItems = inboxItems;
    }
}