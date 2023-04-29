namespace MyTelegram.ReadModel;

public interface IChatInviteReadModel : IReadModel
{
    long AdminId { get; }
    long ChannelId { get; }
    int Date { get; }
    int? ExpireDate { get; }
    string Id { get; }
    string Link { get; }
    bool Permanent { get; }
    bool Revoked { get; }
    int StartDate { get; }
    int Usage { get; }
    int? UsageLimit { get; }
}
