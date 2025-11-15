namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Mark or unmark a sticker as favorite
/// Possible errors
/// Code Type Description
/// 400 STICKER_ID_INVALID The provided sticker ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.faveSticker"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class FaveStickerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestFaveSticker, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestFaveSticker obj)
    {
        throw new NotImplementedException();
    }
}