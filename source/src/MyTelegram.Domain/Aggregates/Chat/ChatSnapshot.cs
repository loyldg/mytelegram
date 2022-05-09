namespace MyTelegram.Domain.Aggregates.Chat;

public class ChatSnapshot : ISnapshot
{
    public ChatSnapshot(long chatId,
        string title,
        long creatorUid,
        IReadOnlyList<ChatMember> chatMemberList,
        long latestSenderPeerId,
        int latestSenderMessageId,
        long latestDeletedUserId,
        ChatBannedRights? defaultBannedRights,
        string? about
    )
    {
        ChatId = chatId;
        Title = title;
        CreatorUid = creatorUid;
        ChatMemberList = chatMemberList;
        LatestSenderPeerId = latestSenderPeerId;
        LatestSenderMessageId = latestSenderMessageId;
        LatestDeletedUserId = latestDeletedUserId;
        DefaultBannedRights = defaultBannedRights;
        About = about;
    }

    public string? About { get; }

    public long ChatId { get; }
    public IReadOnlyList<ChatMember> ChatMemberList { get; }
    public long CreatorUid { get; }
    public ChatBannedRights? DefaultBannedRights { get; }
    public long LatestDeletedUserId { get; }
    public int LatestSenderMessageId { get; }
    public long LatestSenderPeerId { get; }
    public string Title { get; }
}
