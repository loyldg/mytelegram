using TAuthorization = MyTelegram.Schema.TAuthorization;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers.Authorization;

public class AuthorizationMapper : ILayeredMapper,
    IObjectMapper<IDeviceReadModel, TAuthorization>
{
    public TAuthorization Map(IDeviceReadModel source)
    {
        return Map(source, new TAuthorization());
    }

    public TAuthorization Map(IDeviceReadModel source,
        TAuthorization destination)
    {
        destination.ApiId = source.AppId;
        destination.AppName = source.AppName;
        destination.AppVersion = source.AppVersion;
        destination.Hash = source.Hash;
        destination.OfficialApp = source.OfficialApp;
        destination.PasswordPending = source.PasswordPending;
        destination.DeviceModel = source.DeviceModel;
        destination.Platform = source.Platform;
        destination.SystemVersion = source.SystemVersion;
        destination.Ip = source.Ip;
        destination.DateActive = source.DateActive;
        destination.DateCreated = source.DateCreated;

        return destination;
    }
}