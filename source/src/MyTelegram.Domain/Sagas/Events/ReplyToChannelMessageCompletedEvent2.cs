namespace MyTelegram.Domain.Sagas.Events;

public class ReplyToChannelMessageCompletedEvent2 : AggregateEvent<SendMessageSaga, SendMessageSagaId>
{
    public ReplyToChannelMessageCompletedEvent2(
        //        long channelId,
        IInputReplyTo replyTo,
        long channelId,
        int repliesPts,
        int maxId,
        long savedFromPeerId,
        int savedFromMsgId,
        IReadOnlyCollection<Peer> recentRepliers
    )
    {
        //        ReplyToMsgId = replyToMsgId;
        ReplyTo = replyTo;
        ChannelId = channelId;
        RepliesPts = repliesPts;
        MaxId = maxId;
        SavedFromPeerId = savedFromPeerId;
        SavedFromMsgId = savedFromMsgId;
        RecentRepliers = recentRepliers;
    }

    public int ReplyToMsgId { get; }
    public IInputReplyTo ReplyTo { get; }
    public long ChannelId { get; }
    public int RepliesPts { get; }
    public int MaxId { get; }
    public long SavedFromPeerId { get; }
    public int SavedFromMsgId { get; }
    public IReadOnlyCollection<Peer> RecentRepliers { get; }
}