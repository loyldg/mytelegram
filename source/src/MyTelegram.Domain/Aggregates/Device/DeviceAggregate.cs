namespace MyTelegram.Domain.Aggregates.Device;

public class DeviceAggregate : AggregateRoot<DeviceAggregate, DeviceId>
{
    private readonly DeviceState _state = new();

    public DeviceAggregate(DeviceId id) : base(id)
    {
        Register(_state);
    }

    public void BindUidToDevice(long userId,
        long permAuthKeyId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new BindUidToDeviceEvent(userId, permAuthKeyId, DateTime.UtcNow.ToTimestamp()));
    }

    public void Create(
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
        int layer
    )
    {
        //Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        var hash = _state.Hash;
        if (hash == 0)
        {
            hash = Random.Shared.NextInt64();
        }
        
        Emit(new DeviceCreatedEvent(IsNew,
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
            DateTime.UtcNow.ToTimestamp()));
    }

    public void UnRegisterDevice(long permAuthKeyId,
        long tempAuthKeyId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeviceAuthKeyUnRegisteredEvent(permAuthKeyId, tempAuthKeyId));
    }
}
