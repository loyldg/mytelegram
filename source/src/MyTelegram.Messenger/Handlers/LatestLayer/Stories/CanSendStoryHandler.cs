using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.canSendStory" />
///</summary>
internal sealed class CanSendStoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestCanSendStory, ICanSendStoryCount>
{
    protected override Task<ICanSendStoryCount> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestCanSendStory obj)
    {
        return Task.FromResult<ICanSendStoryCount>(new TCanSendStoryCount());
    }
}
