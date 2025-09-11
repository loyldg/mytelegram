namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Fetch all <a href="https://corefork.telegram.org/api/stickers">stickersets »</a> owned by the current user.
/// See <a href="https://corefork.telegram.org/method/messages.getMyStickers" />
///</summary>
internal sealed class GetMyStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMyStickers, MyTelegram.Schema.Messages.IMyStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IMyStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMyStickers obj)
    {
        throw new NotImplementedException();
    }
}
