namespace MyTelegram.Domain.Commands.Chat;

public class AddChatUserCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>, IHasCorrelationId
{
    public AddChatUserCommand(ChatId aggregateId,
        RequestInfo request,
        long userId,
        int date,
        string messageActionData,
        long randomId,
        Guid correlationId) : base(aggregateId, request)
    {
        UserId = userId;
        Date = date;
        MessageActionData = messageActionData;
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    public int Date { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public long UserId { get; }

    public Guid CorrelationId { get; }
}