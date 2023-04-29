namespace MyTelegram.Domain.Commands.Channel;

public class SetChannelPtsCommand : Command<ChannelAggregate, ChannelId, IExecutionResult>
{
    public SetChannelPtsCommand(ChannelId aggregateId,
        long senderPeerId,
        int pts,
        int messageId,
        int date) : base(aggregateId)
    {
        SenderPeerId = senderPeerId;
        Pts = pts;
        MessageId = messageId;
        Date = date;
    }

    public long SenderPeerId { get; }
    public int Pts { get; }
    public int MessageId { get; }
    public int Date { get; }
}
