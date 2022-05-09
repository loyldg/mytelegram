namespace MyTelegram.Domain.Commands.Chat;

public class CheckChatStateCommand : Command<ChatAggregate, ChatId, IExecutionResult>
{
    public Guid CorrelationId { get; }

    public CheckChatStateCommand(ChatId aggregateId, Guid correlationId) : base(aggregateId)
    {
        CorrelationId = correlationId;
    }
}