namespace MyTelegram.Domain.Events.Channel;

public class
    ChannelCreatorCreatedEvent : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId> //, IHasCorrelationId
{
    public ChannelCreatorCreatedEvent(RequestInfo requestInfo,
        long channelId,
        long userId,
        long inviterId,
        int date) : base(requestInfo)
    {
        ChannelId = channelId;
        UserId = userId;
        InviterId = inviterId;
        Date = date;
        //
    }

    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public long UserId { get; }

    //
}