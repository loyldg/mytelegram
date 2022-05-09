using MyTelegram.Handlers.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.MessengerServer.Handlers.Impl.Payments;

public class GetSavedInfoHandler : RpcResultObjectHandler<RequestGetSavedInfo, ISavedInfo>,
    IGetSavedInfoHandler
{
    protected override Task<ISavedInfo> HandleCoreAsync(IRequestInput input,
        RequestGetSavedInfo obj)
    {
        throw new NotImplementedException();
    }
}
