namespace MyTelegram.Domain.Commands.Channel;

public class CheckChannelStateCommand : Command<ChannelAggregate, ChannelId, IExecutionResult>
{
    public CheckChannelStateCommand(ChannelId aggregateId,
        long senderPeerId,
        int messageId,
        int date,
        MessageSubType messageSubType,
        Guid correlationId) : base(aggregateId)
    {
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        Date = date;
        MessageSubType = messageSubType;
        CorrelationId = correlationId;
    }

    public long SenderPeerId { get; }
    public int MessageId { get; }
    public int Date { get; }
    public MessageSubType MessageSubType { get; }
    public Guid CorrelationId { get; }
}
