namespace MyTelegram.Domain.Commands.Dialog;

public class StartClearGroupChatHistoryCommand : Command<ChatAggregate, ChatId, IExecutionResult>,
    IHasCorrelationId
{
    public StartClearGroupChatHistoryCommand(ChatId aggregateId,
        Guid correlationId) : base(aggregateId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
