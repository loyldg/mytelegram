//using MyTelegram.Domain.Sagas;

//namespace MyTelegram.Domain.Events.Messaging;

//public class SendOutboxMessageCompletedEvent : RequestAggregateEvent2<SendMessageSaga, SendMessageSagaId>
//{
//    public MessageItem MessageItem { get; }
//    public List<long>? MentionedUserIds { get; }
//    public int Pts { get; }
//    public int GroupItemCount { get; }
//    public long? LinkedChannelId { get; }
//    public IReadOnlyCollection<long>? BotUserIds { get; }

//    public SendOutboxMessageCompletedEvent(RequestInfo requestInfo, MessageItem messageItem,
//        List<long>? mentionedUserIds,
//        int pts, int groupItemCount,
//        long? linkedChannelId, IReadOnlyCollection<long>? botUserIds) : base(requestInfo)
//    {
//        MessageItem = messageItem;
//        MentionedUserIds = mentionedUserIds;
//        Pts = pts;
//        GroupItemCount = groupItemCount;
//        LinkedChannelId = linkedChannelId;
//        BotUserIds = botUserIds;
//    }
//}