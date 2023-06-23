using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetChatThemeHandler : RpcResultObjectHandler<RequestSetChatTheme, IUpdates>,
    ISetChatThemeHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSetChatTheme obj)
    {
        throw new NotImplementedException();
    }
}