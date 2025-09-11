namespace MyTelegram.Messenger.Handlers.LatestLayer.Stickers;

///<summary>
/// Replace a sticker in a <a href="https://corefork.telegram.org/api/stickers">stickerset »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 STICKER_INVALID The provided sticker is invalid.
/// See <a href="https://corefork.telegram.org/method/stickers.replaceSticker" />
///</summary>
internal sealed class ReplaceStickerHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestReplaceSticker, MyTelegram.Schema.Messages.IStickerSet>
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stickers.RequestReplaceSticker obj)
    {
        throw new NotImplementedException();
    }
}
