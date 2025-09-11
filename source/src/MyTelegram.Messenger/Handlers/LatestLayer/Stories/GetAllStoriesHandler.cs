using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// Fetch the List of active (or active and hidden) stories, see <a href="https://corefork.telegram.org/api/stories#watching-stories">here »</a> for more info on watching stories.
/// See <a href="https://corefork.telegram.org/method/stories.getAllStories" />
///</summary>
internal sealed class GetAllStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetAllStories, MyTelegram.Schema.Stories.IAllStories>
{
    protected override Task<MyTelegram.Schema.Stories.IAllStories> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetAllStories obj)
    {
        return Task.FromResult<MyTelegram.Schema.Stories.IAllStories>(new TAllStories
        {
            Chats = [],
            PeerStories = [],
            Users = [],
            StealthMode = new TStoriesStealthMode { },
            HasMore = false,
            State = "",
        });
    }
}
