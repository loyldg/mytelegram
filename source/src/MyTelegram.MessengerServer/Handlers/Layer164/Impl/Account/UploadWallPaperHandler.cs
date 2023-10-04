using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UploadWallPaperHandler : RpcResultObjectHandler<RequestUploadWallPaper, IWallPaper>,
    IUploadWallPaperHandler
{
    protected override Task<IWallPaper> HandleCoreAsync(IRequestInput input,
        RequestUploadWallPaper obj)
    {
        throw new NotImplementedException();
    }
}