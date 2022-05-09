using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SetContentSettingsHandler : RpcResultObjectHandler<RequestSetContentSettings, IBool>,
    ISetContentSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetContentSettings obj)
    {
        throw new NotSupportedException();
    }
}
