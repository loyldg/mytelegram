using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class ToggleGroupCallRecordHandler : RpcResultObjectHandler<RequestToggleGroupCallRecord, IUpdates>,
    IToggleGroupCallRecordHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleGroupCallRecord obj)
    {
        throw new NotImplementedException();
    }
}