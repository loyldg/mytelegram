using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetChatThemesHandler : RpcResultObjectHandler<RequestGetChatThemes, IChatThemes>,
    IGetChatThemesHandler
{
    protected override Task<IChatThemes> HandleCoreAsync(IRequestInput input,
        RequestGetChatThemes obj)
    {
        throw new NotImplementedException();
    }
}
