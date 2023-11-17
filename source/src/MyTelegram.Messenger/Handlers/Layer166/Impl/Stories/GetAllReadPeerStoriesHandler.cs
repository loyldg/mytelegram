// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getAllReadPeerStories" />
///</summary>
internal sealed class GetAllReadPeerStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetAllReadPeerStories, MyTelegram.Schema.IUpdates>,
    Stories.IGetAllReadPeerStoriesHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetAllReadPeerStories obj)
    {
        throw new NotImplementedException();
    }
}
