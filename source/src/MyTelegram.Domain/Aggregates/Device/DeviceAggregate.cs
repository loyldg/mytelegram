namespace MyTelegram.Domain.Aggregates.Device;

[EnableAutoGeneration]
public class DeviceAggregate : SnapshotAggregateRoot<DeviceAggregate, DeviceId, DeviceSnapshot>
{
    private readonly DeviceState _state = new();

    public DeviceAggregate(DeviceId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void BindUserIdToDevice(long userId,
        long permAuthKeyId)
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new BindUidToDeviceEvent(userId, permAuthKeyId, date));
    }

    public void CreateDevice(
        long permAuthKeyId,
        long tempAuthKeyId,
        long userId,
        int apiId,
        string appName,
        string appVersion,
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
        Dictionary<string, string>? parameters
    )
    {
        //Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        var hash = _state.Hash;
        if (hash == 0)
        {
            hash = Random.Shared.NextInt64();
        }

        var date = _state.DateCreated;
        if (date == 0)
        {
            date = DateTime.UtcNow.ToTimestamp();
        }

        var newParameters = _state.Parameters;
        if (newParameters != null)
        {
            if (parameters != null)
            {
                foreach (var kv in parameters)
                {
                    newParameters.Remove(kv.Key);
                    newParameters.Add(kv.Key, kv.Value);
                }
            }
        }
        parameters = newParameters;
        var isNewDevice = IsNew;
        Emit(new DeviceCreatedEvent(isNewDevice,
            permAuthKeyId,
            tempAuthKeyId,
            userId,
            apiId,
            appName,
            appVersion,
            hash,
            officialApp,
            passwordPending,
            deviceModel,
            platform,
            systemVersion,
            systemLangCode,
            langPack,
            langCode,
            ip,
            layer,
            date,
            parameters));
    }

    public void UnRegisterDeviceForAuthKey(long permAuthKeyId,
        long tempAuthKeyId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeviceAuthKeyUnRegisteredEvent(permAuthKeyId, tempAuthKeyId));
    }

    protected override Task<DeviceSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new DeviceSnapshot(_state.PermAuthKeyId,
            _state.TempAuthKeyId,
            _state.UserId,
            _state.ApiId,
            _state.AppName,
            _state.AppVersion,
            _state.OfficialApp,
            _state.PasswordPending,
            _state.DeviceModel,
            _state.Platform,
            _state.SystemVersion,
            _state.SystemLangCode,
            _state.LangPack,
            _state.LangCode,
            _state.Ip,
            _state.Layer,
            _state.DateCreated,
            _state.IsActive,
            _state.Parameters,
            _state.Hash));
    }

    protected override Task LoadSnapshotAsync(DeviceSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);

        return Task.CompletedTask;
    }
}
