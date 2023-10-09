using System.Collections.Concurrent;

namespace MyTelegram.Domain.Aggregates.Chat;

public class ChatSnapshot : ISnapshot
{
    public ChatSnapshot(long chatId,
        string title,
        long creatorUid,
        long? photoId,
        //IReadOnlyList<ChatMember> chatMemberList,
        List<ChatMember> chatMembers,
        List<ChatAdmin> chatAdmins,
        List<long> botUserIds,
        long latestSenderPeerId,
        int latestSenderMessageId,
        long latestDeletedUserId,
        ChatBannedRights? defaultBannedRights,
        string? about,
        int? ttlPeriod,
        long? migrateToChannelId,
        bool noForwards
    )
    {
        ChatId = chatId;
        Title = title;
        CreatorUid = creatorUid;
        PhotoId = photoId;
        ChatMembers = chatMembers;
        ChatAdmins = chatAdmins;
        BotUserIds = botUserIds;
        //ChatMemberList = chatMemberList;
        LatestSenderPeerId = latestSenderPeerId;
        LatestSenderMessageId = latestSenderMessageId;
        LatestDeletedUserId = latestDeletedUserId;
        DefaultBannedRights = defaultBannedRights;
        About = about;
        TtlPeriod = ttlPeriod;
        MigrateToChannelId = migrateToChannelId;
        NoForwards = noForwards;
    }

    public string? About { get; }
    public int? TtlPeriod { get; }
    public long? MigrateToChannelId { get; }
    public bool NoForwards { get; }

    public long ChatId { get; }
    //public IReadOnlyList<ChatMember> ChatMemberList { get; }
    public long CreatorUid { get; }
    public long? PhotoId { get; }
    public List<ChatMember> ChatMembers { get; }
    public List<ChatAdmin> ChatAdmins { get; }
    public List<long> BotUserIds { get; }
    public ChatBannedRights? DefaultBannedRights { get; }
    public long LatestDeletedUserId { get; }
    public int LatestSenderMessageId { get; }
    public long LatestSenderPeerId { get; }
    public string Title { get; }
}