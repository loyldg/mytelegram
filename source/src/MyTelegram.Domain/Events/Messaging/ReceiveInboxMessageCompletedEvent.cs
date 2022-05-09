using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class ReceiveInboxMessageCompletedEvent : AggregateEvent<MessageSaga, MessageSagaId>
{
    public MessageItem MessageItem { get; }
    public int Pts { get; }
    public string? ChatTitle { get; }

    public ReceiveInboxMessageCompletedEvent(MessageItem messageItem, int pts, string? chatTitle)
    {
        MessageItem = messageItem;
        Pts = pts;
        ChatTitle = chatTitle;
    }
}