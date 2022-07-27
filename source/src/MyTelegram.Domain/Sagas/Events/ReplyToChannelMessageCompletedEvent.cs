namespace MyTelegram.Domain.Sagas.Events;

public class ReplyToChannelMessageCompletedEvent : AggregateEvent<MessageSaga, MessageSagaId>
{
    public ReplyToChannelMessageCompletedEvent(int replyToMsgId,
        long channelId,
        int repliesPts,
        int maxId,
        long savedFromPeerId,
        int savedFromMsgId,
        IReadOnlyCollection<Peer> recentRepliers
        )
    {
        ReplyToMsgId = replyToMsgId;
        ChannelId = channelId;
        RepliesPts = repliesPts;
        MaxId = maxId;
        SavedFromPeerId = savedFromPeerId;
        SavedFromMsgId = savedFromMsgId;
        RecentRepliers = recentRepliers;
    }

    public int ReplyToMsgId { get; }
    public long ChannelId { get; }
    public int RepliesPts { get; }
    public int MaxId { get; }
    public long SavedFromPeerId { get; }
    public int SavedFromMsgId { get; }
    public IReadOnlyCollection<Peer> RecentRepliers { get; }
}
