namespace MyTelegram.Domain.Events.ChatInvite;

public class ChatInviteDeletedEvent : RequestAggregateEvent2<ChatInviteAggregate, ChatInviteId>
{
    public long ChannelId { get; }
    public long InviteId { get; }

    public ChatInviteDeletedEvent(RequestInfo requestInfo,long channelId,long inviteId) : base(requestInfo)
    {
        ChannelId = channelId;
        InviteId = inviteId;
    }
}