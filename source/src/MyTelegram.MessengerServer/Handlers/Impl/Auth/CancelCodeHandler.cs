using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class CancelCodeHandler : RpcResultObjectHandler<RequestCancelCode, IBool>,
    ICancelCodeHandler, IProcessedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCancelCode obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
