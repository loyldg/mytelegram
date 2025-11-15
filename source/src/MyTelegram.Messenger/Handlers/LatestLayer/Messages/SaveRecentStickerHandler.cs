namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Add/remove sticker from recent stickers list
/// Possible errors
/// Code Type Description
/// 400 STICKER_ID_INVALID The provided sticker ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.saveRecentSticker"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveRecentStickerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSaveRecentSticker, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSaveRecentSticker obj)
    {
        throw new NotImplementedException();
    }
}