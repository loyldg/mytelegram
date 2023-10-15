namespace MyTelegram.ReadModel;

public interface IChatAdminReadModel : IReadModel
{
    //long ChannelId { get; }
    long PeerId { get; }
    long PromotedBy { get; }
    bool CanEdit { get; }
    long UserId { get; }
    bool IsBot { get; }
    string? Rank { get; }
    ChatAdminRights AdminRights { get; }
    int Date { get; }
    bool IsCreator { get; }
}