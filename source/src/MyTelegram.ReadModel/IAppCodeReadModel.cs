namespace MyTelegram.ReadModel;

public interface IAppCodeReadModel : IReadModel
{
    string AppCodeId { get; }
    string Code { get; }
    long CreationTime { get; }
    int Expire { get; }
    string Id { get; }
    string PhoneCodeHash { get; }
    string PhoneNumber { get; }
}

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

public interface IEncryptedMessageReadModel : IReadModel
{
}