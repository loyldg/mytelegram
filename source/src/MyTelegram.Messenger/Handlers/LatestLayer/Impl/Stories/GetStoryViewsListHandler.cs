// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getStoryViewsList" />
///</summary>
internal sealed class GetStoryViewsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoryViewsList, MyTelegram.Schema.Stories.IStoryViewsList>,
    Stories.IGetStoryViewsListHandler
{
    protected override Task<MyTelegram.Schema.Stories.IStoryViewsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetStoryViewsList obj)
    {
        throw new NotImplementedException();
    }
}
