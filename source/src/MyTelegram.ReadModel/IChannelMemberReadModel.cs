namespace MyTelegram.ReadModel;

public interface IChannelMemberReadModel : IReadModel
{
    int BannedRights { get; }
    long ChannelId { get; }
    int Date { get; }
    string Id { get; }
    // ReSharper disable once IdentifierTypo
    long InviterId { get; }
    bool IsBot { get; }
    bool Kicked { get; }
    long KickedBy { get; }
    bool Left { get; }
    int UntilDate { get; }
    long UserId { get; }
    long? ChatInviteId { get; }
}