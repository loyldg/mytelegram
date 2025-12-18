namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get stickers attached to a photo or video
/// Possible errors
/// Code Type Description
/// 400 MEDIA_EMPTY The provided media object is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getAttachedStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAttachedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachedStickers, TVector<MyTelegram.Schema.IStickerSetCovered>>
{
    protected override Task<TVector<IStickerSetCovered>> HandleCoreAsync(IRequestInput input, RequestGetAttachedStickers obj)
    {
        var r = new TVector<IStickerSetCovered>();
        return Task.FromResult(r);
    }
}