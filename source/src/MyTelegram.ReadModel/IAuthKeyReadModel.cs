namespace MyTelegram.ReadModel;

public interface IAuthKeyReadModel : IReadModel
{
    long AuthKeyId { get; }
    byte[] Data { get; }
    string Id { get; }
    bool IsActive { get; }
    DateTime LastUpdateTime { get; }
    byte[] ServerSalt { get; }
    byte[] TempAuthKeyData { get; }
    long TempAuthKeyId { get; }
    byte[] TempServerSalt { get; }
    long UserId { get; }
}
