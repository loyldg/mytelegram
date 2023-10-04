using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class HidePromoDataHandler : RpcResultObjectHandler<RequestHidePromoData, IBool>,
    IHidePromoDataHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestHidePromoData obj)
    {
        throw new NotImplementedException();
    }
}