namespace MyTelegram.Domain.Commands.Chat;

public class DeleteChatUserCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>//, IHasCorrelationId
{
    public DeleteChatUserCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        long userId,
        string messageActionData,
        long randomId) : base(aggregateId, requestInfo)
    {
        UserId = userId;
        MessageActionData = messageActionData;
        RandomId = randomId;
        //Date = date; 
    }

    public string MessageActionData { get; }
    public long RandomId { get; }

    public long UserId { get; }

    //public int Date { get; } 
}