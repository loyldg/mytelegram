using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class LogOutHandler : RpcResultObjectHandler<RequestLogOut, IBool>,
    ILogOutHandler, IProcessedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestLogOut obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
        //if (input.IsAuthKeyActive)
        //{
        //    return Task.FromResult<IBool>(new TBoolTrue());

        //}

        //return Task.FromResult<IBool>(new TBoolFalse());
    }
}
