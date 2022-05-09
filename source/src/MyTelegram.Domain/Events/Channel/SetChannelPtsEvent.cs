namespace MyTelegram.Domain.Events.Channel;

public class SetChannelPtsEvent : AggregateEvent<ChannelAggregate, ChannelId>
{
    public SetChannelPtsEvent(long senderPeerId,int pts,int messageId,int date)
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
