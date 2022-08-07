namespace MyTelegram.Domain.Commands.Chat;

public class DeleteChatCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>, IHasCorrelationId
{
    public DeleteChatCommand(ChatId aggregateId,
        RequestInfo request, Guid correlationId) : base(aggregateId, request)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
