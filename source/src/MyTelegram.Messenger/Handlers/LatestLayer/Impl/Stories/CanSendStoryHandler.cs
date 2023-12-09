// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.canSendStory" />
///</summary>
internal sealed class CanSendStoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestCanSendStory, IBool>,
    Stories.ICanSendStoryHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestCanSendStory obj)
    {
        throw new NotImplementedException();
    }
}
