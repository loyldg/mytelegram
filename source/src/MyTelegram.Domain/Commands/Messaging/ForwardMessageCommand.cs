namespace MyTelegram.Domain.Commands.Messaging;

public class ForwardMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public ForwardMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        long randomId,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    public long RandomId { get; }
    public Guid CorrelationId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RandomId);
        yield return CorrelationId.ToByteArray();
    }
}
