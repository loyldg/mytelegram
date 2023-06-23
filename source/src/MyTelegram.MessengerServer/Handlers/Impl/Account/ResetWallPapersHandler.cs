using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ResetWallPapersHandler : RpcResultObjectHandler<RequestResetWallPapers, IBool>,
    IResetWallPapersHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetWallPapers obj)
    {
        throw new NotImplementedException();
    }
}