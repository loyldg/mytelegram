namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.togglePinnedToTop" />
///</summary>
internal sealed class TogglePinnedToTopHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestTogglePinnedToTop, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestTogglePinnedToTop obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
