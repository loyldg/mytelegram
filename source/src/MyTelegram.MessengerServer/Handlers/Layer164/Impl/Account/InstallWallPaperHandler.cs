using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class InstallWallPaperHandler : RpcResultObjectHandler<RequestInstallWallPaper, IBool>,
    IInstallWallPaperHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestInstallWallPaper obj)
    {
        throw new NotImplementedException();
    }
}