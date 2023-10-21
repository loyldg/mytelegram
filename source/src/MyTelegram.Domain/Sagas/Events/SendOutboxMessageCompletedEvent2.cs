namespace MyTelegram.Domain.Sagas.Events;

public class SendOutboxMessageCompletedEvent2 : RequestAggregateEvent2<SendMessageSaga, SendMessageSagaId>
{
    public MessageItem MessageItem { get; }
    public List<long>? MentionedUserIds { get; }
    public int Pts { get; }
    public int GroupItemCount { get; }
    public long? LinkedChannelId { get; }
    public IReadOnlyCollection<long>? BotUserIds { get; }
    //public long GlobalSeqNo { get; }

    public SendOutboxMessageCompletedEvent2(RequestInfo requestInfo, MessageItem messageItem,
        List<long>? mentionedUserIds,
        int pts, int groupItemCount,
        long? linkedChannelId, IReadOnlyCollection<long>? botUserIds/*, long globalSeqNo*/) : base(requestInfo)
    {
        MessageItem = messageItem;
        MentionedUserIds = mentionedUserIds;
        Pts = pts;
        GroupItemCount = groupItemCount;
        LinkedChannelId = linkedChannelId;
        BotUserIds = botUserIds;
        //GlobalSeqNo = globalSeqNo;
    }
}