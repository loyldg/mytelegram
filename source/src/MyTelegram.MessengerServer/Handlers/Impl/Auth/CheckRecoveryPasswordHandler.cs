using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class CheckRecoveryPasswordHandler : RpcResultObjectHandler<RequestCheckRecoveryPassword, IBool>,
    ICheckRecoveryPasswordHandler, IProcessedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCheckRecoveryPassword obj)
    {
        // todo:check recovery password
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
