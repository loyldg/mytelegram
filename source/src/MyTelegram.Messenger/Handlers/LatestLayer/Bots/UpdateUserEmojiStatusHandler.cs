namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Change the emoji status of a user (invoked by bots, see <a href="https://corefork.telegram.org/api/emoji-status#setting-an-emoji-status-from-a-bot">here »</a> for more info on the full flow)
/// Possible errors
/// Code Type Description
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 403 USER_PERMISSION_DENIED The user hasn't granted or has revoked the bot's access to change their emoji status using <a href="https://corefork.telegram.org/method/bots.toggleUserEmojiStatusPermission">bots.toggleUserEmojiStatusPermission</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.updateUserEmojiStatus"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateUserEmojiStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestUpdateUserEmojiStatus, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestUpdateUserEmojiStatus obj)
    {
        throw new NotImplementedException();
    }
}