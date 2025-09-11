namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Returns a list of available <a href="https://corefork.telegram.org/api/wallpapers">wallpapers</a>.
/// See <a href="https://corefork.telegram.org/method/account.getWallPapers" />
///</summary>
internal sealed class GetWallPapersHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWallPapers, MyTelegram.Schema.Account.IWallPapers>
{
    protected override Task<MyTelegram.Schema.Account.IWallPapers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetWallPapers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IWallPapers>(new TWallPapers
        {
            Wallpapers = []
        });
    }
}
