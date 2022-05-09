using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class RecoverPasswordHandler : RpcResultObjectHandler<RequestRecoverPassword, IAuthorization>,
    IRecoverPasswordHandler
{
    protected override Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestRecoverPassword obj)
    {
        throw new NotImplementedException();
    }
}
