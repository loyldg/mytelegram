using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class EditLocationHandler : RpcResultObjectHandler<RequestEditLocation, IBool>,
    IEditLocationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestEditLocation obj)
    {
        throw new NotImplementedException();
    }
}