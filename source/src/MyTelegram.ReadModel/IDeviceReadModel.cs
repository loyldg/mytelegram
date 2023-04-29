namespace MyTelegram.ReadModel;

public interface IDeviceReadModel : IReadModel
{
    int AppId { get; }
    string AppName { get; }
    string AppVersion { get; }
    int DateActive { get; }
    int DateCreated { get; }
    string DeviceModel { get; }
    long Hash { get; }
    string Id { get; }
    string Ip { get; }

    bool IsActive { get; }
    string LangCode { get; }
    string LangPack { get; }
    int Layer { get; }
    bool OfficialApp { get; }
    bool PasswordPending { get; }
    long PermAuthKeyId { get; }
    string Platform { get; }
    string SystemLangCode { get; }
    string SystemVersion { get; }
    long TempAuthKeyId { get; }
    long UserId { get; }
}
