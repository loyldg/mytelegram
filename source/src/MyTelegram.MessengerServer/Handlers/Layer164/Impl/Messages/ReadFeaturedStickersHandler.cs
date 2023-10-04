using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReadFeaturedStickersHandler : RpcResultObjectHandler<RequestReadFeaturedStickers, IBool>,
    IReadFeaturedStickersHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReadFeaturedStickers obj)
    {
        throw new NotImplementedException();
    }
}