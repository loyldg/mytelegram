namespace MyTelegram.Domain.Commands.Chat;

public class CheckChatStateCommand : Command<ChatAggregate, ChatId, IExecutionResult>
{
    public CheckChatStateCommand(ChatId aggregateId,
        Guid correlationId) : base(aggregateId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
