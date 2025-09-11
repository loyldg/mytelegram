namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.readStories" />
///</summary>
internal sealed class ReadStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestReadStories, TVector<int>>
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestReadStories obj)
    {
        throw new NotImplementedException();
    }
}
