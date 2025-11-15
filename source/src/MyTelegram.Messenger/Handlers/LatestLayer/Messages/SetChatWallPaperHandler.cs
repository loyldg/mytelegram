namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Set a custom <a href="https://corefork.telegram.org/api/wallpapers">wallpaper »</a> in a specific private chat with another user.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// 400 WALLPAPER_NOT_FOUND The specified wallpaper could not be found.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.setChatWallPaper"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetChatWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetChatWallPaper, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetChatWallPaper obj)
    {
        throw new NotImplementedException();
    }
}