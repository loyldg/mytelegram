using MyTelegram.Handlers.Stats;
using MyTelegram.Schema.Stats;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stats;

public class GetBroadcastStatsHandler : RpcResultObjectHandler<RequestGetBroadcastStats, IBroadcastStats>,
    IGetBroadcastStatsHandler
{
    protected override Task<IBroadcastStats> HandleCoreAsync(IRequestInput input,
        RequestGetBroadcastStats obj)
    {
        throw new NotImplementedException();
    }
}
