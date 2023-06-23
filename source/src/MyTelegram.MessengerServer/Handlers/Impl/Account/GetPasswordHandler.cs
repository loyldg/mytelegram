using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetPasswordHandler : RpcResultObjectHandler<RequestGetPassword, IPassword>,
    IGetPasswordHandler
{
    protected override Task<IPassword> HandleCoreAsync(IRequestInput input,
        RequestGetPassword obj)
    {
        throw new NotImplementedException();
    }
}