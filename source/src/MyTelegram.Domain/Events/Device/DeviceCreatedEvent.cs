namespace MyTelegram.Domain.Events.Device;

public class DeviceCreatedEvent : AggregateEvent<DeviceAggregate, DeviceId>
{
    public DeviceCreatedEvent(
        bool isNewDevice,
        long permAuthKeyId,
        long tempAuthKeyId,
        long userId,
        int apiId,
        string appName,
        string appVersion,
        long hash,
        bool officialApp,
        bool passwordPending,
        string deviceModel,
        string platform,
        string systemVersion,
        string systemLangCode,
        string langPack,
        string langCode,
        string ip,
        int layer,
        int date
    )
    {
        IsNewDevice = isNewDevice;
        PermAuthKeyId = permAuthKeyId;
        TempAuthKeyId = tempAuthKeyId;
        UserId = userId;
        ApiId = apiId;
        AppName = appName;
        AppVersion = appVersion;
        Hash = hash;
        OfficialApp = officialApp;
        PasswordPending = passwordPending;
        DeviceModel = deviceModel;
        Platform = platform;
        SystemVersion = systemVersion;
        SystemLangCode = systemLangCode;
        LangPack = langPack;
        LangCode = langCode;
        Ip = ip;
        Layer = layer;
        Date = date;
    }

    public int ApiId { get; }
    public string AppName { get; }
    public string AppVersion { get; }
    public int Date { get; }
    public string DeviceModel { get; }
    public long Hash { get; }
    public string Ip { get; }
    public bool IsNewDevice { get; }
    public string LangCode { get; }
    public string LangPack { get; }
    public int Layer { get; }
    public bool OfficialApp { get; }
    public bool PasswordPending { get; }
    public long PermAuthKeyId { get; }
    public string Platform { get; }
    public string SystemLangCode { get; }
    public string SystemVersion { get; }
    public long TempAuthKeyId { get; }
    public long UserId { get; }
}
