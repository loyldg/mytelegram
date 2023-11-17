// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.deleteStories" />
///</summary>
internal sealed class DeleteStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestDeleteStories, TVector<int>>,
    Stories.IDeleteStoriesHandler
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestDeleteStories obj)
    {
        throw new NotImplementedException();
    }
}
