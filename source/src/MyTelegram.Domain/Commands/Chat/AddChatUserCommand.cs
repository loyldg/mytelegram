namespace MyTelegram.Domain.Commands.Chat;

public class AddChatUserCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>//, IHasCorrelationId
{
    public AddChatUserCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        long userId,
        int date,
        string messageActionData,
        long randomId) : base(aggregateId, requestInfo)
    {
        UserId = userId;
        Date = date;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }

    public int Date { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public long UserId { get; }

}
