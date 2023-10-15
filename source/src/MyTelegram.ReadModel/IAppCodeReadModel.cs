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