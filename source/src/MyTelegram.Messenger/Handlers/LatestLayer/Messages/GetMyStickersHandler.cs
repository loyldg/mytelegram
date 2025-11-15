namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Fetch all <a href="https://corefork.telegram.org/api/stickers">stickersets »</a> owned by the current user.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getMyStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetMyStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMyStickers, MyTelegram.Schema.Messages.IMyStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IMyStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetMyStickers obj)
    {
        throw new NotImplementedException();
    }
}