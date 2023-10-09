// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.sendStory" />
///</summary>
internal sealed class SendStoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestSendStory, MyTelegram.Schema.IUpdates>,
    Stories.ISendStoryHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestSendStory obj)
    {
        throw new NotImplementedException();
    }
}
