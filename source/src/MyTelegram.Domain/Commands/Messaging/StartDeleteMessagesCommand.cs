namespace MyTelegram.Domain.Commands.Messaging;

public class StartDeleteMessagesCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public bool Revoke { get; }
    public IReadOnlyList<int> IdList { get; }
    public long? ChatCreatorId { get; }
    public Guid CorrelationId { get; }

    public StartDeleteMessagesCommand(MessageId aggregateId,
        RequestInfo requestInfo, bool revoke,
        IReadOnlyList<int> idList,
        long? chatCreatorId,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        Revoke = revoke;
        IdList = idList;
        ChatCreatorId = chatCreatorId;
        CorrelationId = correlationId;
    }
}