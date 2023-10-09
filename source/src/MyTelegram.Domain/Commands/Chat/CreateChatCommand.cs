namespace MyTelegram.Domain.Commands.Chat;

public class CreateChatCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>//, IHasCorrelationId
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
        int? ttlPeriod = null
    ) : base(aggregateId, requestInfo)
    {
        ChatId = chatId;
        CreatorUid = creatorUid;
        Title = title;
        MemberUidList = memberUidList;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        TtlPeriod = ttlPeriod;
    }

    public long ChatId { get; }
    public long CreatorUid { get; }
    public int Date { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public string MessageActionData { get; }
    public int? TtlPeriod { get; }
    public long RandomId { get; }
    public string Title { get; }
}