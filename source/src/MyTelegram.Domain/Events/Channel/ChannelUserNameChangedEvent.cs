namespace MyTelegram.Domain.Events.Channel;

public class ChannelUserNameChangedEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>, IHasCorrelationId
{
    public ChannelUserNameChangedEvent(long reqMsgId,
        long channelId,
        string userName,
        string? oldUserName,
        Guid correlationId) : base(reqMsgId)
    {
        ChannelId = channelId;
        UserName = userName;
        OldUserName = oldUserName;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public string? OldUserName { get; }
    public string UserName { get; }
    public Guid CorrelationId { get; }
}
