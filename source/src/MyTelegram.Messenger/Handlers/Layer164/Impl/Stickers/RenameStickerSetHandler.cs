// ReSharper disable All

namespace MyTelegram.Handlers.Stickers;

///<summary>
/// Renames a stickerset, bots only.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 STICKERSET_INVALID The provided sticker set is invalid.
/// See <a href="https://corefork.telegram.org/method/stickers.renameStickerSet" />
///</summary>
internal sealed class RenameStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestRenameStickerSet, MyTelegram.Schema.Messages.IStickerSet>,
    Stickers.IRenameStickerSetHandler
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stickers.RequestRenameStickerSet obj)
    {
        throw new NotImplementedException();
    }
}
