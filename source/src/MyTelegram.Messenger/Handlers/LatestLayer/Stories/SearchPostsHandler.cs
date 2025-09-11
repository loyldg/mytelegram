using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.searchPosts" />
///</summary>
internal sealed class SearchPostsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestSearchPosts, MyTelegram.Schema.Stories.IFoundStories>
{
    protected override Task<MyTelegram.Schema.Stories.IFoundStories> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestSearchPosts obj)
    {
        return Task.FromResult<IFoundStories>(new TFoundStories
        {
            Chats = [],
            Stories = [],
            Users = []
        });
    }
}
