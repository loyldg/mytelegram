using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ResendCodeHandler : RpcResultObjectHandler<RequestResendCode, ISentCode>,
    IResendCodeHandler
{
    protected override Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestResendCode obj)
    {
        throw new NotImplementedException();
    }
}
