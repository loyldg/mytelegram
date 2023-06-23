using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetAuthorizationFormHandler : RpcResultObjectHandler<RequestGetAuthorizationForm, IAuthorizationForm>,
    IGetAuthorizationFormHandler
{
    protected override Task<IAuthorizationForm> HandleCoreAsync(IRequestInput input,
        RequestGetAuthorizationForm obj)
    {
        throw new NotImplementedException();
    }
}