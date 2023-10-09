namespace MyTelegram.ReadModel;

public interface IChatInviteReadModel : IReadModel
{
    string Id { get; }
    long InviteId { get; }
    long AdminId { get; }
    //long ChannelId { get; }
    long PeerId { get; }
    string? Title { get; }
    bool RequestNeeded { get; }
    int Date { get; }
    int? ExpireDate { get; }
    string Link { get; set; }
    bool Permanent { get; }
    bool Revoked { get; }
    int StartDate { get; }
    int Usage { get; }
    int? UsageLimit { get; }
    int? Requested { get; }
}