namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Install <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// See <a href="https://corefork.telegram.org/method/account.installWallPaper" />
///</summary>
internal sealed class InstallWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInstallWallPaper, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestInstallWallPaper obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());   }
}
