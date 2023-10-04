using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UpdateThemeHandler : RpcResultObjectHandler<RequestUpdateTheme, ITheme>,
    IUpdateThemeHandler
{
    protected override Task<ITheme> HandleCoreAsync(IRequestInput input,
        RequestUpdateTheme obj)
    {
        throw new NotImplementedException();
    }
}