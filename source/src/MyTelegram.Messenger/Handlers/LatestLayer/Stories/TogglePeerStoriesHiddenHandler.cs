namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.togglePeerStoriesHidden" />
///</summary>
internal sealed class TogglePeerStoriesHiddenHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestTogglePeerStoriesHidden, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestTogglePeerStoriesHidden obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
