using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SaveGifHandler : RpcResultObjectHandler<RequestSaveGif, IBool>,
    ISaveGifHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveGif obj)
    {
        throw new NotImplementedException();
    }
}
