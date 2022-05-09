namespace MyTelegram.Domain.Commands.Chat;

public class ReadLatestNoneBotOutboxMessageCommand : Command<ChatAggregate, ChatId, IExecutionResult>, IHasCorrelationId
{
    public ReadLatestNoneBotOutboxMessageCommand(ChatId aggregateId,
        string sourceCommandId,
        Guid correlationId) : base(aggregateId)
    {
        SourceCommandId = sourceCommandId;
        CorrelationId = correlationId;
    }

    public string SourceCommandId { get; }
    public Guid CorrelationId { get; }
}
