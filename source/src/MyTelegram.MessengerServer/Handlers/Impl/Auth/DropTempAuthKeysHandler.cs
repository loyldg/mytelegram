using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class DropTempAuthKeysHandler : RpcResultObjectHandler<RequestDropTempAuthKeys, IBool>,
    IDropTempAuthKeysHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDropTempAuthKeys obj)
    {
        throw new NotImplementedException();
    }
}