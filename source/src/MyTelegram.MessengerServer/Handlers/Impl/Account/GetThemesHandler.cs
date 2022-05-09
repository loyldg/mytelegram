using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetThemesHandler : RpcResultObjectHandler<RequestGetThemes, IThemes>,
    IGetThemesHandler, IProcessedHandler
{
    protected override Task<IThemes> HandleCoreAsync(IRequestInput input,
        RequestGetThemes obj)
    {
        var r = new TThemes { Themes = new TVector<ITheme>(), Hash = obj.Hash };

        return Task.FromResult<IThemes>(r);
    }
}
