// ReSharper disable All

namespace MyTelegram.Handlers.Stickers;

///<summary>
/// Deletes a stickerset we created, bots only.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_MISSING Only bots can call this method, please use <a href="https://t.me/stickers">@stickers</a> if you're a user.
/// 400 STICKERSET_INVALID The provided sticker set is invalid.
/// See <a href="https://corefork.telegram.org/method/stickers.deleteStickerSet" />
///</summary>
internal sealed class DeleteStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestDeleteStickerSet, IBool>,
    Stickers.IDeleteStickerSetHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stickers.RequestDeleteStickerSet obj)
    {
        throw new NotImplementedException();
    }
}
