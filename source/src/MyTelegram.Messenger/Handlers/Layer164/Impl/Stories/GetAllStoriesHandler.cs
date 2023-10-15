// ReSharper disable All

using MyTelegram.Schema.Stories;

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getAllStories" />
///</summary>
internal sealed class GetAllStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetAllStories, MyTelegram.Schema.Stories.IAllStories>,
    Stories.IGetAllStoriesHandler
{
    protected override Task<MyTelegram.Schema.Stories.IAllStories> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetAllStories obj)
    {
        return Task.FromResult<MyTelegram.Schema.Stories.IAllStories>(new TAllStories
        {
            Chats = new(),
            PeerStories = new(),
            State = "1697097729:0:0:573180153705f49484",
            StealthMode = new TStoriesStealthMode { },
            Count = 0,
            HasMore = false,
            Users = new(),
        });
    }
}
