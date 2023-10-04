using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetWebAuthorizationsHandler : RpcResultObjectHandler<RequestGetWebAuthorizations, IWebAuthorizations>,
    IGetWebAuthorizationsHandler
{
    protected override Task<IWebAuthorizations> HandleCoreAsync(IRequestInput input,
        RequestGetWebAuthorizations obj)
    {
        throw new NotImplementedException();
    }
}