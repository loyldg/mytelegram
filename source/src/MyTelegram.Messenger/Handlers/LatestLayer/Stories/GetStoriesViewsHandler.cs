namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getStoriesViews" />
///</summary>
internal sealed class GetStoriesViewsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoriesViews, MyTelegram.Schema.Stories.IStoryViews>
{
    protected override Task<MyTelegram.Schema.Stories.IStoryViews> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetStoriesViews obj)
    {
        throw new NotImplementedException();
    }
}
