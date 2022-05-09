namespace MyTelegram.Domain.Events.Channel;

public class
    ChannelCreatorCreatedEvent : RequestAggregateEvent<ChannelMemberAggregate, ChannelMemberId> //, IHasCorrelationId
{
    public ChannelCreatorCreatedEvent(long reqMsgId,
        long channelId,
        long userId,
        long inviterId,
        int date) : base(reqMsgId)
    {
        ChannelId = channelId;
        UserId = userId;
        InviterId = inviterId;
        Date = date;
        //CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public long UserId { get; }

    //public Guid CorrelationId { get; }
}
