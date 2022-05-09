using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetWallPaperHandler : RpcResultObjectHandler<RequestGetWallPaper, IWallPaper>,
    IGetWallPaperHandler
{
    protected override Task<IWallPaper> HandleCoreAsync(IRequestInput input,
        RequestGetWallPaper obj)
    {
        throw new NotImplementedException();
    }
}
