using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class SendOutboxMessageCompletedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
{
    public MessageItem MessageItem { get; }
    public int Pts { get; }
    public int GroupItemCount { get; }

    public SendOutboxMessageCompletedEvent(RequestInfo request, MessageItem messageItem, int pts, int groupItemCount) : base(request)
    {
        MessageItem = messageItem;
        Pts = pts;
        GroupItemCount = groupItemCount;
    }
}