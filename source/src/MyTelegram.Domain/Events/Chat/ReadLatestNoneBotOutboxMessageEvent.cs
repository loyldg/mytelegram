namespace MyTelegram.Domain.Events.Chat;

public class ReadLatestNoneBotOutboxMessageEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ReadLatestNoneBotOutboxMessageEvent(
        RequestInfo requestInfo,
        long chatId,
        long senderPeerId,
        int senderMessageId,
        string sourceCommandId) : base(requestInfo)
    {
        ChatId = chatId;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        SourceCommandId = sourceCommandId;

    }

    public long ChatId { get; }
    public int SenderMessageId { get; }
    public long SenderPeerId { get; }
    public string SourceCommandId { get; }

}