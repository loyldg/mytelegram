namespace MyTelegram.Domain.Commands.Messaging;

public class ForwardMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public long RandomId { get; }
    public ForwardMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        long randomId) : base(aggregateId, requestInfo)
    {
        RandomId = randomId;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RandomId);
        yield return RequestInfo.RequestId.ToByteArray();
    }
}