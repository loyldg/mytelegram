using MyTelegram.Handlers.Help;

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
