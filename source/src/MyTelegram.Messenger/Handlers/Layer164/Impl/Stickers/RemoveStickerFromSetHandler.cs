// ReSharper disable All

namespace MyTelegram.Handlers.Stickers;

///<summary>
/// Remove a sticker from the set where it belongs, bots only. The sticker set must have been created by the bot.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_MISSING Only bots can call this method, please use <a href="https://t.me/stickers">@stickers</a> if you're a user.
/// 400 STICKER_INVALID The provided sticker is invalid.
/// See <a href="https://corefork.telegram.org/method/stickers.removeStickerFromSet" />
///</summary>
internal sealed class RemoveStickerFromSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestRemoveStickerFromSet, MyTelegram.Schema.Messages.IStickerSet>,
    Stickers.IRemoveStickerFromSetHandler
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stickers.RequestRemoveStickerFromSet obj)
    {
        throw new NotImplementedException();
    }
}
