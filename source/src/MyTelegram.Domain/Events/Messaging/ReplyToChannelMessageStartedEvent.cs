using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class ReplyToChannelMessageStartedEvent : AggregateEvent<MessageSaga, MessageSagaId>
{
    public int ReplyToMsgId { get; }
    public long SavedFromPeerId { get; }
    public int SavedFromMsgId { get; }
    public IReadOnlyCollection<Peer> RecentRepliers { get; }

    public ReplyToChannelMessageStartedEvent(int replyToMsgId, long savedFromPeerId,int savedFromMsgId,IReadOnlyCollection<Peer> recentRepliers)
    {
        ReplyToMsgId = replyToMsgId;
        SavedFromPeerId = savedFromPeerId;
        SavedFromMsgId = savedFromMsgId;
        RecentRepliers = recentRepliers;
    }
}