// ReSharper disable All

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
        throw new NotImplementedException();
    }
}
