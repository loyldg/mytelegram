using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class SendChannelMessageStartedEvent : AggregateEvent<MessageSaga, MessageSagaId>
{
    public SendChannelMessageStartedEvent(bool post,
        int? views,
        IReadOnlyList<long> botUidList,
        long? linkedChannelId,
        Guid correlationId)
    {
        Post = post;
        Views = views;
        BotUidList = botUidList;
        LinkedChannelId = linkedChannelId;
        CorrelationId = correlationId;
    }

    public bool Post { get; }
    public int? Views { get; }
    public IReadOnlyList<long> BotUidList { get; }
    public long? LinkedChannelId { get; }
    public Guid CorrelationId { get; }
}
