using MyTelegram.Handlers.Stats;
using MyTelegram.Schema.Stats;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stats;

public class GetMegagroupStatsHandler : RpcResultObjectHandler<RequestGetMegagroupStats, IMegagroupStats>,
    IGetMegagroupStatsHandler
{
    protected override Task<IMegagroupStats> HandleCoreAsync(IRequestInput input,
        RequestGetMegagroupStats obj)
    {
        throw new NotImplementedException();
    }
}