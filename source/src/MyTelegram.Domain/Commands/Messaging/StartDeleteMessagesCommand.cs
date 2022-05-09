namespace MyTelegram.Domain.Commands.Messaging;

public class StartDeleteMessagesCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public bool Revoke { get; }
    public IReadOnlyList<int> IdList { get; }
    public Guid CorrelationId { get; }

    public StartDeleteMessagesCommand(MessageId aggregateId,
        RequestInfo request, bool revoke,
        IReadOnlyList<int> idList,
        Guid correlationId) : base(aggregateId, request)
    {
        Revoke = revoke;
        IdList = idList;
        CorrelationId = correlationId;
    }
}