namespace MyTelegram.Domain.Events.Channel;

public class ChannelUserNameChangedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelUserNameChangedEvent(RequestInfo requestInfo,
        long channelId,
        string userName,
        string? oldUserName) : base(requestInfo)
    {
        ChannelId = channelId;
        UserName = userName;
        OldUserName = oldUserName;
    }

    public long ChannelId { get; }
    public string? OldUserName { get; }
    public string UserName { get; }
}