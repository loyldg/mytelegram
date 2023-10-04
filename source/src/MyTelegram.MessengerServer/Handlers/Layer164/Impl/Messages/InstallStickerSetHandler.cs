using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class InstallStickerSetHandler : RpcResultObjectHandler<RequestInstallStickerSet, IStickerSetInstallResult>,
    IInstallStickerSetHandler
{
    protected override Task<IStickerSetInstallResult> HandleCoreAsync(IRequestInput input,
        RequestInstallStickerSet obj)
    {
        throw new NotImplementedException();
    }
}