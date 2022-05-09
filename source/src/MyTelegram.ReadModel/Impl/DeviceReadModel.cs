namespace MyTelegram.ReadModel.Impl;

public class DeviceReadModel : IDeviceReadModel,
    IAmReadModelFor<DeviceAggregate, DeviceId, DeviceCreatedEvent>,
    IAmReadModelFor<DeviceAggregate, DeviceId, BindUidToDeviceEvent>,
    IAmReadModelFor<DeviceAggregate, DeviceId, DeviceAuthKeyUnRegisteredEvent>
//IAmReadModelFor<AuthKeyAggregate, AuthKeyId, BindTempAuthKeyToPermanentAuthKeyEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DeviceAggregate, DeviceId, BindUidToDeviceEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = DeviceId.Create(domainEvent.AggregateEvent.PermAuthKeyId).Value;
        }

        UserId = domainEvent.AggregateEvent.UserId;
        DateActive = domainEvent.AggregateEvent.Date;
        IsActive = true;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DeviceAggregate, DeviceId, DeviceAuthKeyUnRegisteredEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        IsActive = false;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DeviceAggregate, DeviceId, DeviceCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        PermAuthKeyId = domainEvent.AggregateEvent.PermAuthKeyId;
        TempAuthKeyId = domainEvent.AggregateEvent.TempAuthKeyId;
        UserId = domainEvent.AggregateEvent.UserId;
        AppId = domainEvent.AggregateEvent.AppId;
        AppName = domainEvent.AggregateEvent.AppName;
        AppVersion = domainEvent.AggregateEvent.AppVersion;
        Hash = domainEvent.AggregateEvent.Hash;
        OfficialApp = domainEvent.AggregateEvent.OfficialApp;
        PasswordPending = domainEvent.AggregateEvent.PasswordPending;
        DeviceModel = domainEvent.AggregateEvent.DeviceModel;
        Platform = domainEvent.AggregateEvent.Platform;
        SystemVersion = domainEvent.AggregateEvent.SystemVersion;
        SystemLangCode = domainEvent.AggregateEvent.SystemLangCode;
        LangPack = domainEvent.AggregateEvent.LangPack;
        LangCode = domainEvent.AggregateEvent.LangCode;
        Ip = domainEvent.AggregateEvent.Ip;
        Layer = domainEvent.AggregateEvent.Layer;

        if (domainEvent.AggregateEvent.IsNewDevice)
        {
            DateCreated = domainEvent.AggregateEvent.Date;
        }

        DateActive = DateCreated;

        IsActive = true;
        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;

    public virtual long PermAuthKeyId { get; private set; }
    public virtual long TempAuthKeyId { get; private set; }
    public virtual long UserId { get; private set; }
    public virtual int AppId { get; private set; }
    public virtual string AppName { get; private set; } = null!;
    public virtual string AppVersion { get; private set; } = null!;
    public virtual long Hash { get; private set; }
    public virtual bool OfficialApp { get; private set; }
    public virtual bool PasswordPending { get; private set; }
    public virtual string DeviceModel { get; private set; } = null!;
    public virtual string Platform { get; private set; } = null!;
    public virtual string SystemVersion { get; private set; } = null!;
    public virtual string SystemLangCode { get; private set; } = null!;
    public virtual string LangPack { get; private set; } = null!;
    public virtual string LangCode { get; private set; } = null!;
    public virtual string Ip { get; private set; } = null!;
    public virtual int Layer { get; private set; }
    public virtual int DateCreated { get; private set; }
    public virtual int DateActive { get; private set; }
    public virtual bool IsActive { get; private set; }
}
