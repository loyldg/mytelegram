using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetThemeHandler : RpcResultObjectHandler<RequestGetTheme, ITheme>,
    IGetThemeHandler
{
    protected override Task<ITheme> HandleCoreAsync(IRequestInput input,
        RequestGetTheme obj)
    {
        throw new NotImplementedException();
    }
}
