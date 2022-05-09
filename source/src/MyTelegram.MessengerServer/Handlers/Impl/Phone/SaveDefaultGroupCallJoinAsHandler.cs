using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class SaveDefaultGroupCallJoinAsHandler : RpcResultObjectHandler<RequestSaveDefaultGroupCallJoinAs, IBool>,
    ISaveDefaultGroupCallJoinAsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveDefaultGroupCallJoinAs obj)
    {
        throw new NotImplementedException();
    }
}
