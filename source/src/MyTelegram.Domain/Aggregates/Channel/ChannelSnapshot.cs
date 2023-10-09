namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelSnapshot : ISnapshot
{
    public ChannelSnapshot(bool broadcast,
        //bool megaGroup,
        long channelId,
        long creatorUid,
        long? photoId,
        bool preHistoryHidden,
        int maxMessageId,
        IReadOnlyList<long> botUidList,
        long latestNoneBotSenderPeerId,
        int latestNoneBotSenderMessageId,
        ChatBannedRights? defaultBannedRights,
        int slowModeSeconds,
        int lastSendDate,
        //long lastSenderPeerId,
        IReadOnlyList<ChatAdmin> adminList,
        int pinnedMsgId,
        byte[]? photo,
        long? linkedChannelId,
        string? userName,
        bool forum,
        int maxTopicId,
        int? ttlPeriod,
        long? migratedFromChatId,
        int? migratedMaxId,
        bool noForwards,
        bool isFirstChatInviteCreated,
        int? requestsPending,
        List<long>? recentRequesters,
        bool signatureEnabled,
        int participantCount
            )
    {
        Broadcast = broadcast;
        //MegaGroup = megaGroup;
        ChannelId = channelId;
        CreatorUid = creatorUid;
        PhotoId = photoId;
        PreHistoryHidden = preHistoryHidden;
        MaxMessageId = maxMessageId;
        BotUidList = botUidList;
        LatestNoneBotSenderPeerId = latestNoneBotSenderPeerId;
        LatestNoneBotSenderMessageId = latestNoneBotSenderMessageId;
        DefaultBannedRights = defaultBannedRights;
        SlowModeSeconds = slowModeSeconds;
        LastSendDate = lastSendDate;
        //LastSenderPeerId = lastSenderPeerId;
        AdminList = adminList;
        PinnedMsgId = pinnedMsgId;
        Photo = photo;
        LinkedChannelId = linkedChannelId;
        UserName = userName;
        Forum = forum;
        MaxTopicId = maxTopicId;
        TtlPeriod = ttlPeriod;
        MigratedFromChatId = migratedFromChatId;
        MigratedMaxId = migratedMaxId;
        NoForwards = noForwards;
        IsFirstChatInviteCreated = isFirstChatInviteCreated;
        RequestsPending = requestsPending;
        RecentRequesters = recentRequesters;
        SignatureEnabled = signatureEnabled;
        ParticipantCount = participantCount;
    }

    //public long LastSenderPeerId { get; private set; }
    public IReadOnlyList<ChatAdmin> AdminList { get; }
    public IReadOnlyList<long> BotUidList { get; }

    public bool Broadcast { get; }

    //public bool MegaGroup { get; }
    public long ChannelId { get; }

    public long CreatorUid { get; }
    public long? PhotoId { get; }
    public ChatBannedRights? DefaultBannedRights { get; }

    public int LastSendDate { get; }
    public int LatestNoneBotSenderMessageId { get; }

    public long LatestNoneBotSenderPeerId { get; }
    public long? LinkedChannelId { get; }
    public int MaxMessageId { get; }
    public byte[]? Photo { get; }
    public int PinnedMsgId { get; }
    public bool PreHistoryHidden { get; }
    public int SlowModeSeconds { get; }
    public string? UserName { get; }
    public bool Forum { get; }
    public int MaxTopicId { get; }
    public int? TtlPeriod { get; }
    public long? MigratedFromChatId { get; }
    public int? MigratedMaxId { get; }
    public bool NoForwards { get; }
    public bool IsFirstChatInviteCreated { get; }
    public int? RequestsPending { get; }
    public List<long>? RecentRequesters { get; }
    public bool SignatureEnabled { get; }
    public int ParticipantCount { get; }
}
