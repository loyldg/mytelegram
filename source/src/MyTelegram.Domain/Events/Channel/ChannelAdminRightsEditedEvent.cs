namespace MyTelegram.Domain.Events.Channel;

public class ChannelAdminRightsEditedEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>
{
    public ChannelAdminRightsEditedEvent(long reqMsgId,
        long channelId,
        long promotedBy,
        bool canEdit,
        long userId,
        ChatAdminRights adminRights,
        string rank) : base(reqMsgId)
    {
        ChannelId = channelId;
        PromotedBy = promotedBy;
        CanEdit = canEdit;
        UserId = userId;
        AdminRights = adminRights;
        Rank = rank;
    }

    public ChatAdminRights AdminRights { get; }
    public bool CanEdit { get; }

    public long ChannelId { get; }
    public long PromotedBy { get; }

    /// <summary>
    ///     The role (rank) of the admin in the group: just an arbitrary string, admin by default
    /// </summary>
    public string Rank { get; }

    public long UserId { get; }
}
