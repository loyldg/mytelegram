namespace MyTelegram.Domain.Events.Chat;

public class ChatMemberDeletedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatMemberDeletedEvent(
        RequestInfo requestInfo,
        long chatId,
        long userId,
        string messageActionData,
        long randomId
    ) : base(requestInfo)
    {
        ChatId = chatId;
        UserId = userId;
        MessageActionData = messageActionData;
        RandomId = randomId;
        //Date = date; 
    }

    public long ChatId { get; }
    //public int Date { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }

    public long UserId { get; }

}