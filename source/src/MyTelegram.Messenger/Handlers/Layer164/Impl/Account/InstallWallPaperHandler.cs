// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Install <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// See <a href="https://corefork.telegram.org/method/account.installWallPaper" />
///</summary>
internal sealed class InstallWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInstallWallPaper, IBool>,
    Account.IInstallWallPaperHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestInstallWallPaper obj)
    {
        throw new NotImplementedException();
    }
}
