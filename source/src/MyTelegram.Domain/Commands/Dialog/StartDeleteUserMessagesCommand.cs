namespace MyTelegram.Domain.Commands.Dialog;

public class StartDeleteUserMessagesCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public bool Revoke { get; }
    public List<int> MessageIds { get; }
    public bool IsClearHistory { get; }
    public Guid CorrelationId { get; }

    public StartDeleteUserMessagesCommand(DialogId aggregateId,
        RequestInfo requestInfo, bool revoke, List<int> messageIds, bool isClearHistory, Guid correlationId) : base(aggregateId, requestInfo)
    {
        Revoke = revoke;
        MessageIds = messageIds;
        IsClearHistory = isClearHistory;
        CorrelationId = correlationId;
    }
}