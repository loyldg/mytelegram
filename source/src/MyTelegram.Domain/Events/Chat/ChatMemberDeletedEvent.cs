namespace MyTelegram.Domain.Events.Chat;

public class ChatMemberDeletedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>, IHasCorrelationId
{
    public ChatMemberDeletedEvent(
        RequestInfo requestInfo,
        long chatId,
        long userId,
        string messageActionData,
        long randomId,
        //int date,
        Guid correlationId) : base(requestInfo)
    { 
        ChatId = chatId;
        UserId = userId;
        MessageActionData = messageActionData;
        RandomId = randomId;
        //Date = date;
        CorrelationId = correlationId;
    }

    public long ChatId { get; }
    //public int Date { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    
    public long UserId { get; }
    public Guid CorrelationId { get; }
}
