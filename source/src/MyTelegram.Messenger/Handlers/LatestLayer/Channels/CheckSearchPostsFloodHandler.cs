namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.checkSearchPostsFlood" />
///</summary>
internal sealed class CheckSearchPostsFloodHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestCheckSearchPostsFlood, MyTelegram.Schema.ISearchPostsFlood>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.ISearchPostsFlood> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestCheckSearchPostsFlood obj)
    {
        return Task.FromResult<ISearchPostsFlood>(new TSearchPostsFlood
        {
            QueryIsFree = true,
            Remains = 10,
            TotalDaily = 100,
            StarsAmount = 10000
        });
    }
}