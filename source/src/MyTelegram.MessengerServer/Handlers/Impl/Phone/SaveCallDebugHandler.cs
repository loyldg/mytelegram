using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class SaveCallDebugHandler : RpcResultObjectHandler<RequestSaveCallDebug, IBool>,
    ISaveCallDebugHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveCallDebug obj)
    {
        throw new NotImplementedException();
    }
}
