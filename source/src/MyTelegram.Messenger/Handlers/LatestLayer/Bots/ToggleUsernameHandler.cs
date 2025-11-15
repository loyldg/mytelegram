namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Activate or deactivate a purchased <a href="https://fragment.com/">fragment.com</a> username associated to a bot we own.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.toggleUsername"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestToggleUsername, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestToggleUsername obj)
    {
        throw new NotImplementedException();
    }
}