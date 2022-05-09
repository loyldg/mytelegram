namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelSnapshot : ISnapshot
{
    public ChannelSnapshot(bool broadcast,
        //bool megaGroup,
        long channelId,
        long creatorUid,
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
        string? userName)
    {
        Broadcast = broadcast;
        //MegaGroup = megaGroup;
        ChannelId = channelId;
        CreatorUid = creatorUid;
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
    }

    //public long LastSenderPeerId { get; private set; }
    public IReadOnlyList<ChatAdmin> AdminList { get; }
    public IReadOnlyList<long> BotUidList { get; }

    public bool Broadcast { get; }

    //public bool MegaGroup { get; }
    public long ChannelId { get; }

    public long CreatorUid { get; }
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
}
