namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Allow or prevent a bot from <a href="https://corefork.telegram.org/api/emoji-status#setting-an-emoji-status-from-a-bot">changing our emoji status »</a>
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.toggleUserEmojiStatusPermission"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleUserEmojiStatusPermissionHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestToggleUserEmojiStatusPermission, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestToggleUserEmojiStatusPermission obj)
    {
        throw new NotImplementedException();
    }
}