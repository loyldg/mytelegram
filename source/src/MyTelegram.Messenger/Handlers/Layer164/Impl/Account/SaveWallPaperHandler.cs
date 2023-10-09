// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Install/uninstall <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// See <a href="https://corefork.telegram.org/method/account.saveWallPaper" />
///</summary>
internal sealed class SaveWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveWallPaper, IBool>,
    Account.ISaveWallPaperHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveWallPaper obj)
    {
        throw new NotImplementedException();
    }
}
