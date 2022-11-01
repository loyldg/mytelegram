namespace MyTelegram.Domain.Commands.Chat;

public class EditChatTitleCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>, IHasCorrelationId
{
    public EditChatTitleCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        string title,
        string messageActionData,
        long randomId,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        Title = title;
        MessageActionData = messageActionData;
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }
    public Guid CorrelationId { get; }
}
