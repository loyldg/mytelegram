using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SaveWallPaperHandler : RpcResultObjectHandler<RequestSaveWallPaper, IBool>,
    ISaveWallPaperHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveWallPaper obj)
    {
        throw new NotImplementedException();
    }
}
