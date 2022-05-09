namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryReadLatestNoneBotOutboxMessageEvent : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
{
    public ReadHistoryReadLatestNoneBotOutboxMessageEvent(long senderPeerId)
    {
        SenderPeerId = senderPeerId;
    }

    public long SenderPeerId { get; }
}
