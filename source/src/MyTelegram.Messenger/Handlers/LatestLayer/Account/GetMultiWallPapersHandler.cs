namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get info about multiple <a href="https://corefork.telegram.org/api/wallpapers">wallpapers</a>
/// Possible errors
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getMultiWallPapers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetMultiWallPapersHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetMultiWallPapers, TVector<MyTelegram.Schema.IWallPaper>>
{
    protected override Task<TVector<MyTelegram.Schema.IWallPaper>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetMultiWallPapers obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.IWallPaper>>([]);
    }
}