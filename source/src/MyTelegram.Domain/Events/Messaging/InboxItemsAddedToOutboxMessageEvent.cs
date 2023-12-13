namespace MyTelegram.Domain.Events.Messaging;

public class InboxItemsAddedToOutboxMessageEvent : AggregateEvent<MessageAggregate, MessageId>
{
    public List<InboxItem> InboxItems { get; }

    public InboxItemsAddedToOutboxMessageEvent(List<InboxItem> inboxItems)
    {
        InboxItems = inboxItems;
    }
}