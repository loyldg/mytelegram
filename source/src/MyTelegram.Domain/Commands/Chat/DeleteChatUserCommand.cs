namespace MyTelegram.Domain.Commands.Chat;

public class DeleteChatUserCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>, IHasCorrelationId
{
    public DeleteChatUserCommand(ChatId aggregateId,
        RequestInfo request,
        long userId,
        string messageActionData,
        long randomId,
        //int date,
        Guid correlationId) : base(aggregateId, request)
    {
        UserId = userId;
        MessageActionData = messageActionData;
        RandomId = randomId;
        //Date = date;
        CorrelationId = correlationId;
    }

    public string MessageActionData { get; }
    public long RandomId { get; }

    public long UserId { get; }

    //public int Date { get; }
    public Guid CorrelationId { get; }
}
