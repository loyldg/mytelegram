using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class LogOutHandler : RpcResultObjectHandler<RequestLogOut, ILoggedOut>,
    ILogOutHandler, IProcessedHandler
{
    protected override Task<ILoggedOut> HandleCoreAsync(IRequestInput input,
        RequestLogOut obj)
    {
        return Task.FromResult<ILoggedOut>(new TLoggedOut());
        //if (input.IsAuthKeyActive)
        //{
        //    return Task.FromResult<IBool>(new TBoolTrue());

        //}

        //return Task.FromResult<IBool>(new TBoolFalse());
    }
}
