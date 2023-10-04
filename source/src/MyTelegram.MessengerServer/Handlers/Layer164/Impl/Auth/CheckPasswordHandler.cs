using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class CheckPasswordHandler : RpcResultObjectHandler<RequestCheckPassword, IAuthorization>,
    ICheckPasswordHandler
{
    protected override Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestCheckPassword obj)
    {
        throw new NotImplementedException();
    }
}