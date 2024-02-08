namespace MyTelegram.Domain.Aggregates.ChatInvite;

public class ChatInviteSnapshot : ISnapshot
{
    public ChatInviteSnapshot(long channelId, long inviteId, string hash, long adminId, string? title, bool requestNeeded,
        int? startDate, int? expireDate, int? usageLimit, bool permanent, bool revoked, int? usage, int? requested)
    {
        ChannelId = channelId;
        InviteId = inviteId;
        Hash = hash;
        AdminId = adminId;
        Title = title;
        RequestNeeded = requestNeeded;
        StartDate = startDate;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        Permanent = permanent;
        Revoked = revoked;
        Usage = usage;
        Requested = requested;
    }

    public long ChannelId { get; }
    public long InviteId { get; }
    public string Hash { get; }
    public long AdminId { get; }
    public string? Title { get; }
    public bool RequestNeeded { get; }
    public int? StartDate { get; }
    public int? ExpireDate { get; }
    public int? UsageLimit { get; }
    public bool Permanent { get; }
    public bool Revoked { get; }
    public int? Usage { get; }
    public int? Requested { get; }
}