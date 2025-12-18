namespace MyTelegram.Messenger.Handlers.LatestLayer.Stickers;
/// <summary>
/// Deletes a stickerset we created.
/// Possible errors
/// Code Type Description
/// 400 STICKERSET_INVALID The provided sticker set is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stickers.deleteStickerSet"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestDeleteStickerSet, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stickers.RequestDeleteStickerSet obj)
    {
        throw new NotImplementedException();
    }
}