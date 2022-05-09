using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetMultiWallPapersHandler : RpcResultObjectHandler<RequestGetMultiWallPapers, TVector<IWallPaper>>,
    IGetMultiWallPapersHandler
{
    protected override Task<TVector<IWallPaper>> HandleCoreAsync(IRequestInput input,
        RequestGetMultiWallPapers obj)
    {
        throw new NotImplementedException();
    }
}
