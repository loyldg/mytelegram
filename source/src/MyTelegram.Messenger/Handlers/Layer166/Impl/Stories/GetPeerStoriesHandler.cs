// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getPeerStories" />
///</summary>
internal sealed class GetPeerStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetPeerStories, MyTelegram.Schema.Stories.IPeerStories>,
    Stories.IGetPeerStoriesHandler
{
    protected override Task<MyTelegram.Schema.Stories.IPeerStories> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetPeerStories obj)
    {
        throw new NotImplementedException();
    }
}
