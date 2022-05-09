using MyTelegram.Handlers.Stats;
using MyTelegram.Schema.Stats;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stats;

public class LoadAsyncGraphHandler : RpcResultObjectHandler<RequestLoadAsyncGraph, IStatsGraph>,
    ILoadAsyncGraphHandler
{
    protected override Task<IStatsGraph> HandleCoreAsync(IRequestInput input,
        RequestLoadAsyncGraph obj)
    {
        throw new NotImplementedException();
    }
}
