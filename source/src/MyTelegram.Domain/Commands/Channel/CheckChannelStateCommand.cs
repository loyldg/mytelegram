namespace MyTelegram.Domain.Commands.Channel;

public class CheckChannelStateCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public long SenderPeerId { get; }
    public int MessageId { get; }
    public int Date { get; }
    public MessageSubType MessageSubType { get; }

    public CheckChannelStateCommand(ChannelId aggregateId, RequestInfo requestInfo, long senderPeerId, int messageId, int date, MessageSubType messageSubType) : base(aggregateId, requestInfo)
    {
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        Date = date;
        MessageSubType = messageSubType;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return RequestInfo.RequestId.ToByteArray();
    }
}