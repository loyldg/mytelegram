using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetPromoDataHandler : RpcResultObjectHandler<RequestGetPromoData, IPromoData>,
    IGetPromoDataHandler, IProcessedHandler
{
    protected override Task<IPromoData> HandleCoreAsync(IRequestInput input,
        RequestGetPromoData obj)
    {
        IPromoData r = new TPromoDataEmpty { Expires = (int)DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() };

        return Task.FromResult(r);
    }
}
