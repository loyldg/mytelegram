namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberBannedRightsChangedEvent : RequestAggregateEvent<ChannelMemberAggregate, ChannelMemberId>
{
    public ChannelMemberBannedRightsChangedEvent(long reqMsgId,
        long adminId,
        long channelId,
        long memberUid,
        bool needRemoveFromKicked,
        bool needRemoveFromBanned,
        ChatBannedRights bannedRights) : base(reqMsgId)
    {
        AdminId = adminId;
        ChannelId = channelId;
        MemberUid = memberUid;
        NeedRemoveFromKicked = needRemoveFromKicked;
        NeedRemoveFromBanned = needRemoveFromBanned;
        BannedRights = bannedRights;
    }

    public long AdminId { get; }
    public ChatBannedRights BannedRights { get; }
    public long ChannelId { get; }
    public long MemberUid { get; }
    public bool NeedRemoveFromBanned { get; }
    public bool NeedRemoveFromKicked { get; }
}
