using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getStoriesArchive" />
///</summary>
internal sealed class GetStoriesArchiveHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoriesArchive, MyTelegram.Schema.Stories.IStories>
{
    protected override Task<MyTelegram.Schema.Stories.IStories> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetStoriesArchive obj)
    {
        return Task.FromResult<MyTelegram.Schema.Stories.IStories>(new TStories
        {
            Chats = new(),
            Stories = new(),
            Users = new(),
        });
    }
}
