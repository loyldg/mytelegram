using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SaveThemeHandler : RpcResultObjectHandler<RequestSaveTheme, IBool>,
    ISaveThemeHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveTheme obj)
    {
        throw new NotImplementedException();
    }
}
