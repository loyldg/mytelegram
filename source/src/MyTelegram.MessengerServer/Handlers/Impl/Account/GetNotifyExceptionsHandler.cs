using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetNotifyExceptionsHandler : RpcResultObjectHandler<RequestGetNotifyExceptions, IUpdates>,
    IGetNotifyExceptionsHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetNotifyExceptions obj)
    {
        throw new NotImplementedException();
    }
}
