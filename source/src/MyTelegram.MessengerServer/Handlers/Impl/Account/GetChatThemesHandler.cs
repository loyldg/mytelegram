using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetChatThemesHandler : RpcResultObjectHandler<RequestGetChatThemes, IThemes>,
    IGetChatThemesHandler
{
    protected override Task<IThemes> HandleCoreAsync(IRequestInput input,
        RequestGetChatThemes obj)
    {
        throw new NotImplementedException();
    }
}
