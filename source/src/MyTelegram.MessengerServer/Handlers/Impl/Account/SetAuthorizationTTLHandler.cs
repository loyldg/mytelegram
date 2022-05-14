// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class SetAuthorizationTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetAuthorizationTTL, IBool>,
    Account.ISetAuthorizationTTLHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSetAuthorizationTTL obj)
    {
        throw new NotImplementedException();
    }
}
