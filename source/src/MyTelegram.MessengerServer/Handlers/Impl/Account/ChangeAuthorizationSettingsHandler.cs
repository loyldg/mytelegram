// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class ChangeAuthorizationSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestChangeAuthorizationSettings, IBool>,
    Account.IChangeAuthorizationSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestChangeAuthorizationSettings obj)
    {
        throw new NotImplementedException();
    }
}
