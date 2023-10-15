namespace MyTelegram.Domain.Commands.Device;

public class CreateDeviceCommand : RequestCommand2<DeviceAggregate, DeviceId, IExecutionResult>
{
    public CreateDeviceCommand(DeviceId aggregateId,
        RequestInfo requestInfo,
        long permAuthKeyId,
        long tempAuthKeyId,
        long userId,
        int appId,
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
        int layer
    ) : base(aggregateId, requestInfo)
    {
        PermAuthKeyId = permAuthKeyId;
        TempAuthKeyId = tempAuthKeyId;
        UserId = userId;
        AppId = appId;
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
    }

    public int AppId { get; }
    public string AppName { get; }
    public string AppVersion { get; }
    public string DeviceModel { get; }
    public long Hash { get; }
    public string Ip { get; }
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

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(PermAuthKeyId);
        yield return BitConverter.GetBytes(Hash);
    }
}
