// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getStoryReactionsList" />
///</summary>
internal sealed class GetStoryReactionsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoryReactionsList, MyTelegram.Schema.Stories.IStoryReactionsList>,
    Stories.IGetStoryReactionsListHandler
{
    protected override Task<MyTelegram.Schema.Stories.IStoryReactionsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetStoryReactionsList obj)
    {
        throw new NotImplementedException();
    }
}
