// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class ChangeAuthorizationSettingsHandler : RpcResultObjectHandler<RequestChangeAuthorizationSettings, IBool>,
    Account.IChangeAuthorizationSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestChangeAuthorizationSettings obj)
    {
        throw new NotImplementedException();
    }
}