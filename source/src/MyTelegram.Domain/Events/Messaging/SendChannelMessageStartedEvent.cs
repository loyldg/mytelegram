using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class SendChannelMessageStartedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
{
    public bool Post { get; }
    public int? Views { get; }
    public IReadOnlyList<long> BotUidList { get; }
    public long? LinkedChannelId { get; }
    public SendChannelMessageStartedEvent(RequestInfo requestInfo, bool post,
        int? views,
        IReadOnlyList<long> botUidList,
        long? linkedChannelId) : base(requestInfo)
    {
        Post = post;
        Views = views;
        BotUidList = botUidList;
        LinkedChannelId = linkedChannelId;
    }
}