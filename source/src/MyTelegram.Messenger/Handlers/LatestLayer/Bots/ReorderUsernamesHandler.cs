namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Reorder usernames associated to a bot we own.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.reorderUsernames"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestReorderUsernames, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestReorderUsernames obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}