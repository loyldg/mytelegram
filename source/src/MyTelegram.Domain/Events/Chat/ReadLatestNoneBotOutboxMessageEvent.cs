namespace MyTelegram.Domain.Events.Chat;

public class ReadLatestNoneBotOutboxMessageEvent : AggregateEvent<ChatAggregate, ChatId>, IHasCorrelationId
{
    public ReadLatestNoneBotOutboxMessageEvent(
        long chatId,
        long senderPeerId,
        int senderMessageId,
        string sourceCommandId,
        Guid correlationId)
    {
        ChatId = chatId;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        SourceCommandId = sourceCommandId;
        CorrelationId = correlationId;
    }

    public long ChatId { get; }
    public int SenderMessageId { get; }
    public long SenderPeerId { get; }
    public string SourceCommandId { get; }
    public Guid CorrelationId { get; }
}
