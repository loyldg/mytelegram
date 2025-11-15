namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Delete all installed <a href="https://corefork.telegram.org/api/wallpapers">wallpapers</a>, reverting to the default wallpaper set.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.resetWallPapers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ResetWallPapersHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetWallPapers, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestResetWallPapers obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}