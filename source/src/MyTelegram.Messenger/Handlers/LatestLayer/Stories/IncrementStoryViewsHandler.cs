namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.incrementStoryViews" />
///</summary>
internal sealed class IncrementStoryViewsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestIncrementStoryViews, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestIncrementStoryViews obj)
    {
        throw new NotImplementedException();
    }
}
