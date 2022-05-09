using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class SetBotUpdatesStatusHandler : RpcResultObjectHandler<RequestSetBotUpdatesStatus, IBool>,
    ISetBotUpdatesStatusHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotUpdatesStatus obj)
    {
        throw new NotImplementedException();
    }
}
