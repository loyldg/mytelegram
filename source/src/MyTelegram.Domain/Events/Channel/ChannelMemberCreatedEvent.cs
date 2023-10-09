namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberCreatedEvent : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId>
{
    public ChannelMemberCreatedEvent( //long reqMsgId,
        RequestInfo requestInfo,
        long channelId,
        long userId,
        long inviterId,
        int date,
        bool isRejoin,
        ChatBannedRights? bannedRights,
        bool isBot,
        long? chatInviteId) : base(requestInfo)
    {
        ChannelId = channelId;
        UserId = userId;
        InviterId = inviterId;
        Date = date;
        IsRejoin = isRejoin;
        BannedRights = bannedRights;
        IsBot = isBot;
        ChatInviteId = chatInviteId;
    }

    public ChatBannedRights? BannedRights { get; }
    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public bool IsBot { get; }
    public long? ChatInviteId { get; }
    public bool IsRejoin { get; }
    public long UserId { get; }
}