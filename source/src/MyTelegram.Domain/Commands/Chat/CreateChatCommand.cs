namespace MyTelegram.Domain.Commands.Chat;

public class CreateChatCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>, IHasCorrelationId
{
    public CreateChatCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        long chatId,
        long creatorUid,
        string title,
        IReadOnlyList<long> memberUidList,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        ChatId = chatId;
        CreatorUid = creatorUid;
        Title = title;
        MemberUidList = memberUidList;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        CorrelationId = correlationId;
    }

    public long ChatId { get; }
    public long CreatorUid { get; }
    public int Date { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }
    public Guid CorrelationId { get; }
}
