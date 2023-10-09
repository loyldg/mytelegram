// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get info about a certain <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getWallPaper" />
///</summary>
internal sealed class GetWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWallPaper, MyTelegram.Schema.IWallPaper>,
    Account.IGetWallPaperHandler
{
    protected override Task<MyTelegram.Schema.IWallPaper> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetWallPaper obj)
    {
        throw new NotImplementedException();
    }
}
