// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class SetAuthorizationTTLHandler : RpcResultObjectHandler<RequestSetAuthorizationTTL, IBool>,
    Account.ISetAuthorizationTTLHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetAuthorizationTTL obj)
    {
        throw new NotImplementedException();
    }
}
