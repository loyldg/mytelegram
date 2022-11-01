using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class SendOutboxMessageCompletedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
{
    public MessageItem MessageItem { get; }
    public int Pts { get; }
    public int GroupItemCount { get; }
    public long? LinkedChannelId { get; }

    public SendOutboxMessageCompletedEvent(RequestInfo requestInfo, MessageItem messageItem, int pts, int groupItemCount,
        long? linkedChannelId) : base(requestInfo)
    {
        MessageItem = messageItem;
        Pts = pts;
        GroupItemCount = groupItemCount;
        LinkedChannelId = linkedChannelId;
    }
}