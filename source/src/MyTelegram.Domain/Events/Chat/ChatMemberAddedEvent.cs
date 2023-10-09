namespace MyTelegram.Domain.Events.Chat;

public class ChatMemberAddedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatMemberAddedEvent(RequestInfo requestInfo,
        long chatId,
        ChatMember chatMember,
        string messageActionData,
        long randomId,
        List<long> allChatMembers) : base(requestInfo)
    {
        ChatId = chatId;
        ChatMember = chatMember;
        MessageActionData = messageActionData;
        RandomId = randomId;
        AllChatMembers = allChatMembers;
    }

    public long ChatId { get; }
    public ChatMember ChatMember { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public List<long> AllChatMembers { get; }
}