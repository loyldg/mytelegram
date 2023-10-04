using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SetAccountTtlHandler : RpcResultObjectHandler<RequestSetAccountTTL, IBool>,
    ISetAccountTTLHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetAccountTTL obj)
    {
        throw new NotSupportedException();
    }
}