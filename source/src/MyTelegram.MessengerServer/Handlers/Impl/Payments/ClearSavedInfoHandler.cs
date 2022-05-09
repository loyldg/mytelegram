using MyTelegram.Handlers.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.MessengerServer.Handlers.Impl.Payments;

public class ClearSavedInfoHandler : RpcResultObjectHandler<RequestClearSavedInfo, IBool>,
    IClearSavedInfoHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestClearSavedInfo obj)
    {
        throw new NotImplementedException();
    }
}
