namespace MyTelegram.Domain.Events.Channel;

public class ChannelAdminRightsEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelAdminRightsEditedEvent(RequestInfo requestInfo,
        long channelId,
        long promotedBy,
        bool canEdit,
        long userId,
        bool isBot,
        bool isNewAdmin,
        ChatAdminRights adminRights,
        string rank,
        bool removeAdminFromList,
        int date) : base(requestInfo)
    {
        ChannelId = channelId;
        PromotedBy = promotedBy;
        CanEdit = canEdit;
        UserId = userId;
        IsBot = isBot;
        IsNewAdmin = isNewAdmin;
        AdminRights = adminRights;
        Rank = rank;
        RemoveAdminFromList = removeAdminFromList;
        Date = date;
    }

    public ChatAdminRights AdminRights { get; }
    public bool CanEdit { get; }

    public long ChannelId { get; }
    public long PromotedBy { get; }

    /// <summary>
    ///     The role (rank) of the admin in the group: just an arbitrary string, admin by default
    /// </summary>
    public string Rank { get; }

    public bool RemoveAdminFromList { get; }
    public int Date { get; }

    public long UserId { get; }
    public bool IsBot { get; }
    public bool IsNewAdmin { get; }
}