namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelMemberSnapshot(
    bool banned,
    ChatBannedRights? bannedRights,
    bool kicked,
    long kickedBy,
    bool left,
    bool isBot,
    bool broadcast,
    int untilDate,
    string? rank,
    bool isAdmin = false,
    int version = 0
    )
    : ISnapshot
{
    public bool IsAdmin { get; } = isAdmin;
    public int Version { get; } = version;
    public bool Banned { get; } = banned;

    public ChatBannedRights? BannedRights { get; } = bannedRights;

    public bool Kicked { get; } = kicked;
    public long KickedBy { get; } = kickedBy;
    public bool Left { get; } = left;
    public bool IsBot { get; } = isBot;
    public bool Broadcast { get; } = broadcast;
    public int UntilDate { get; } = untilDate;
    public string? Rank { get; } = rank;
}