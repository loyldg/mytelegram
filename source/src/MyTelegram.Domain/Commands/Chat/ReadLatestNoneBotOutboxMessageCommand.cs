namespace MyTelegram.Domain.Commands.Chat;

public class ReadLatestNoneBotOutboxMessageCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>
{
    public ReadLatestNoneBotOutboxMessageCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        string sourceCommandId) : base(aggregateId, requestInfo)
    {
        SourceCommandId = sourceCommandId;
    }

    public string SourceCommandId { get; }
}