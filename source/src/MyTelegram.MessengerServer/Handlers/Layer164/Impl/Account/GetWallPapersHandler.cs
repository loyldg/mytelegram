using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetWallPapersHandler : RpcResultObjectHandler<RequestGetWallPapers, IWallPapers>,
    IGetWallPapersHandler
{
    protected override Task<IWallPapers> HandleCoreAsync(IRequestInput input,
        RequestGetWallPapers obj)
    {
        throw new NotImplementedException();
    }
}