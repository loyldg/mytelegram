using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class StartBotHandler : RpcResultObjectHandler<RequestStartBot, IUpdates>,
    IStartBotHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestStartBot obj)
    {
        throw new NotImplementedException();
    }
}
