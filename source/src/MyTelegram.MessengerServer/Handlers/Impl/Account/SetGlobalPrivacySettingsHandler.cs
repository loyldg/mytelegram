using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SetGlobalPrivacySettingsHandler :
    RpcResultObjectHandler<RequestSetGlobalPrivacySettings, IGlobalPrivacySettings>,
    ISetGlobalPrivacySettingsHandler
{
    protected override Task<IGlobalPrivacySettings> HandleCoreAsync(IRequestInput input,
        RequestSetGlobalPrivacySettings obj)
    {
        throw new NotImplementedException();
    }
}
