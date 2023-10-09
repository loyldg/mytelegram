// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get info about multiple <a href="https://corefork.telegram.org/api/wallpapers">wallpapers</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getMultiWallPapers" />
///</summary>
internal sealed class GetMultiWallPapersHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetMultiWallPapers, TVector<MyTelegram.Schema.IWallPaper>>,
    Account.IGetMultiWallPapersHandler
{
    protected override Task<TVector<MyTelegram.Schema.IWallPaper>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetMultiWallPapers obj)
    {
        throw new NotImplementedException();
    }
}
