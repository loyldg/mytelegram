namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryInboxHasReadEvent : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
{
    public ReadHistoryInboxHasReadEvent(bool isOut,
        bool senderIsBot,
        bool needReadLatestNoneBotOutboxMessage)
    {
        IsOut = isOut;
        SenderIsBot = senderIsBot;
        NeedReadLatestNoneBotOutboxMessage = needReadLatestNoneBotOutboxMessage;
    }

    public bool IsOut { get; }
    public bool NeedReadLatestNoneBotOutboxMessage { get; }
    public bool SenderIsBot { get; }
}
