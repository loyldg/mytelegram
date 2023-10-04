using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

// ReSharper disable once UnusedMember.Global
public class AcceptAuthorizationHandler : RpcResultObjectHandler<RequestAcceptAuthorization, IBool>,
    IAcceptAuthorizationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestAcceptAuthorization obj)
    {
        throw new NotImplementedException();
    }
}