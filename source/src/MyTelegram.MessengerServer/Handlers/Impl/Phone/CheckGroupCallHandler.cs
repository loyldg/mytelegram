using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class CheckGroupCallHandler : RpcResultObjectHandler<RequestCheckGroupCall, TVector<int>>,
    ICheckGroupCallHandler
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        RequestCheckGroupCall obj)
    {
        throw new NotImplementedException();
    }
}