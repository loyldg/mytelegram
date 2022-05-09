using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetStatsUrlHandler : RpcResultObjectHandler<RequestGetStatsURL, IStatsURL>,
    IGetStatsURLHandler
{
    protected override Task<IStatsURL> HandleCoreAsync(IRequestInput input,
        RequestGetStatsURL obj)
    {
        throw new NotImplementedException();
    }
}
