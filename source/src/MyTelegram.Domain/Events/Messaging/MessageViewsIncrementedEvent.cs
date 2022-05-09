namespace MyTelegram.Domain.Events.Messaging;

public class MessageViewsIncrementedEvent : AggregateEvent<MessageAggregate, MessageId>
{
    public int MessageId { get; }
    public int Views { get; }

    public MessageViewsIncrementedEvent(int messageId, int views)
    {
        MessageId = messageId;
        Views = views;
    }
}