using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UninstallStickerSetHandler : RpcResultObjectHandler<RequestUninstallStickerSet, IBool>,
    IUninstallStickerSetHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUninstallStickerSet obj)
    {
        throw new NotImplementedException();
    }
}