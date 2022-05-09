using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class SetStickersHandler : RpcResultObjectHandler<RequestSetStickers, IBool>,
    ISetStickersHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetStickers obj)
    {
        throw new NotImplementedException();
    }
}
