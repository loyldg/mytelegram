// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Install a stickerset
/// <para>Possible errors</para>
/// Code Type Description
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.installStickerSet" />
///</summary>
internal sealed class InstallStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestInstallStickerSet, MyTelegram.Schema.Messages.IStickerSetInstallResult>,
    Messages.IInstallStickerSetHandler
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSetInstallResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestInstallStickerSet obj)
    {
        throw new NotImplementedException();
    }
}
