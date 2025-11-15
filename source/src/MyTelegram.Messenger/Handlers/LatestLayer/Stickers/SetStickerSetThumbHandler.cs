namespace MyTelegram.Messenger.Handlers.LatestLayer.Stickers;
/// <summary>
/// Set stickerset thumbnail
/// Possible errors
/// Code Type Description
/// 400 STICKERSET_INVALID The provided sticker set is invalid.
/// 400 STICKER_THUMB_PNG_NOPNG Incorrect stickerset thumb file provided, PNG / WEBP expected.
/// 400 STICKER_THUMB_TGS_NOTGS Incorrect stickerset TGS thumb file provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/stickers.setStickerSetThumb"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SetStickerSetThumbHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestSetStickerSetThumb, MyTelegram.Schema.Messages.IStickerSet>
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stickers.RequestSetStickerSetThumb obj)
    {
        throw new NotImplementedException();
    }
}