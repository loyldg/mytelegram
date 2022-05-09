using MyTelegram.Handlers.Stats;
using MyTelegram.Schema.Stats;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stats;

public class GetMessageStatsHandler : RpcResultObjectHandler<RequestGetMessageStats, IMessageStats>,
    IGetMessageStatsHandler
{
    protected override Task<IMessageStats> HandleCoreAsync(IRequestInput input,
        RequestGetMessageStats obj)
    {
        throw new NotImplementedException();
    }
}
