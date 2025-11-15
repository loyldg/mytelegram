namespace MyTelegram.Messenger.Handlers.LatestLayer.Stickers;
/// <summary>
/// Renames a stickerset.
/// Possible errors
/// Code Type Description
/// 400 STICKERSET_INVALID The provided sticker set is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stickers.renameStickerSet"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class RenameStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestRenameStickerSet, MyTelegram.Schema.Messages.IStickerSet>
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stickers.RequestRenameStickerSet obj)
    {
        throw new NotImplementedException();
    }
}