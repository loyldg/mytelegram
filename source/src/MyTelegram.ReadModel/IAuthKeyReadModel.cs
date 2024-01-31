namespace MyTelegram.ReadModel;

public interface IAuthKeyReadModel : IReadModel
{
    long AuthKeyId { get; }
    byte[] Data { get; }
    string Id { get; }
    bool IsActive { get; }
    DateTime LastUpdateTime { get; }
    long ServerSalt { get; }
    byte[] TempAuthKeyData { get; }
    long TempAuthKeyId { get; }
    long TempServerSalt { get; }
    long UserId { get; }
}
