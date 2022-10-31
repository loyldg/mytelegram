namespace MyTelegram.Domain.Commands.Chat;

public class StartDeleteChatMessagesCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>
{
    public List<int> MessageIds { get; }
    public bool Revoke { get; }
    public bool IsClearHistory { get; }
    public Guid CorrelationId { get; }

    public StartDeleteChatMessagesCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        List<int> messageIds,
        bool revoke,
        bool isClearHistory,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        MessageIds = messageIds;
        Revoke = revoke;
        IsClearHistory = isClearHistory;
        CorrelationId = correlationId;
    }
}