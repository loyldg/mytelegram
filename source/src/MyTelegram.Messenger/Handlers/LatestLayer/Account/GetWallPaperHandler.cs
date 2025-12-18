namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get info about a certain <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>
/// Possible errors
/// Code Type Description
/// 400 WALLPAPER_INVALID The specified wallpaper is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getWallPaper"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWallPaper, MyTelegram.Schema.IWallPaper>
{
    protected override Task<MyTelegram.Schema.IWallPaper> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetWallPaper obj)
    {
        throw new NotImplementedException();
    }
}