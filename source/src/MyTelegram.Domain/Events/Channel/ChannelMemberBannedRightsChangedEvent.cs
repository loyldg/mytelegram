namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberBannedRightsChangedEvent : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId>, IHasCorrelationId
{
    public ChannelMemberBannedRightsChangedEvent(RequestInfo requestInfo,
        long adminId,
        long channelId,
        long memberUid,
        bool kicked,
        long kickedBy,
        bool left,
        bool banned,
        bool removedFromKicked,
        bool removedFromBanned,
        ChatBannedRights bannedRights) : base(requestInfo)
    {
        AdminId = adminId;
        ChannelId = channelId;
        MemberUid = memberUid;
        Kicked = kicked;
        KickedBy = kickedBy;
        Left = left;
        Banned = banned;
        RemovedFromKicked = removedFromKicked;
        RemovedFromBanned = removedFromBanned;
        BannedRights = bannedRights;
    }

    public long AdminId { get; }
    public ChatBannedRights BannedRights { get; }
    public long ChannelId { get; }
    public long MemberUid { get; }
    public bool Kicked { get; }
    public long KickedBy { get; }
    public bool Left { get; }

    public bool Banned { get; }
    public bool RemovedFromKicked { get; }
    public bool RemovedFromBanned { get; }
}