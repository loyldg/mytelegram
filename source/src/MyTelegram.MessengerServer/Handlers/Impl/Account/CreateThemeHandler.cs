using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class CreateThemeHandler : RpcResultObjectHandler<RequestCreateTheme, ITheme>,
    ICreateThemeHandler
{
    protected override Task<ITheme> HandleCoreAsync(IRequestInput input,
        RequestCreateTheme obj)
    {
        throw new NotImplementedException();
    }
}