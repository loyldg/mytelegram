namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Install <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>
/// Possible errors
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.installWallPaper"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class InstallWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInstallWallPaper, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestInstallWallPaper obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}