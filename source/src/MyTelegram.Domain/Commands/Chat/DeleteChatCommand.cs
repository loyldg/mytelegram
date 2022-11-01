namespace MyTelegram.Domain.Commands.Chat;

public class DeleteChatCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>, IHasCorrelationId
{
    public DeleteChatCommand(ChatId aggregateId,
        RequestInfo requestInfo, Guid correlationId) : base(aggregateId, requestInfo)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
