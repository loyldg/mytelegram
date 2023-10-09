// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Uninstall a stickerset
/// <para>Possible errors</para>
/// Code Type Description
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.uninstallStickerSet" />
///</summary>
internal sealed class UninstallStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUninstallStickerSet, IBool>,
    Messages.IUninstallStickerSetHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUninstallStickerSet obj)
    {
        throw new NotImplementedException();
    }
}
