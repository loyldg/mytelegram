namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getStoriesByID" />
///</summary>
internal sealed class GetStoriesByIDHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoriesByID, MyTelegram.Schema.Stories.IStories>
{
    protected override Task<MyTelegram.Schema.Stories.IStories> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetStoriesByID obj)
    {
        throw new NotImplementedException();
    }
}
