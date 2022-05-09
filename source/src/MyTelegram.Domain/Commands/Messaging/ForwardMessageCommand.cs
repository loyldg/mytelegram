namespace MyTelegram.Domain.Commands.Messaging;

public class ForwardMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public long RandomId { get; }
    public Guid CorrelationId { get; }

    public ForwardMessageCommand(MessageId aggregateId,
        RequestInfo request,
        long randomId,
        Guid correlationId) : base(aggregateId, request)
    {
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RandomId);
        yield return CorrelationId.ToByteArray();
    }
}