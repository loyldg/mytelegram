// ReSharper disable All

namespace MyTelegram.Handlers.Stickers;

///<summary>
/// Add a sticker to a stickerset, bots only. The sticker set must have been created by the bot.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_MISSING Only bots can call this method, please use <a href="https://t.me/stickers">@stickers</a> if you're a user.
/// 400 STICKERPACK_STICKERS_TOO_MUCH There are too many stickers in this stickerpack, you can't add any more.
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// 400 STICKERS_TOO_MUCH There are too many stickers in this stickerpack, you can't add any more.
/// 400 STICKER_PNG_NOPNG One of the specified stickers is not a valid PNG file.
/// 400 STICKER_TGS_NOTGS Invalid TGS sticker provided.
/// See <a href="https://corefork.telegram.org/method/stickers.addStickerToSet" />
///</summary>
internal sealed class AddStickerToSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestAddStickerToSet, MyTelegram.Schema.Messages.IStickerSet>,
    Stickers.IAddStickerToSetHandler
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stickers.RequestAddStickerToSet obj)
    {
        throw new NotImplementedException();
    }
}
