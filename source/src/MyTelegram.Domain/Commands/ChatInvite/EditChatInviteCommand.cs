namespace MyTelegram.Domain.Commands.ChatInvite;

public class EditChatInviteCommand : RequestCommand2<ChatInviteAggregate, ChatInviteId, IExecutionResult>
{
    public EditChatInviteCommand(ChatInviteId aggregateId, RequestInfo requestInfo, long channelId, long inviteId,
        string hash, string? newHash, long adminId, string? title, bool requestNeeded, int? startDate, int? expireDate, int? usageLimit,
        bool permanent, bool revoked) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        InviteId = inviteId;
        Hash = hash;
        NewHash = newHash;
        AdminId = adminId;
        Title = title;
        RequestNeeded = requestNeeded;
        StartDate = startDate;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        Permanent = permanent;
        Revoked = revoked;
    }

    public long ChannelId { get; }
    public long InviteId { get; }
    public string Hash { get; }
    public string? NewHash { get; }
    public long AdminId { get; }
    public string? Title { get; }
    public bool RequestNeeded { get; }
    public int? StartDate { get; }
    public int? ExpireDate { get; }
    public int? UsageLimit { get; }
    public bool Permanent { get; }
    public bool Revoked { get; }
}