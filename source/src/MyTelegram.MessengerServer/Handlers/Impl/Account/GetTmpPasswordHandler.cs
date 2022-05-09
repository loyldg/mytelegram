using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetTmpPasswordHandler : RpcResultObjectHandler<RequestGetTmpPassword, ITmpPassword>,
    IGetTmpPasswordHandler
{
    protected override Task<ITmpPassword> HandleCoreAsync(IRequestInput input,
        RequestGetTmpPassword obj)
    {
        throw new NotImplementedException();
    }
}
