namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.sendReaction" />
///</summary>
internal sealed class SendReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestSendReaction, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestSendReaction obj)
    {
        throw new NotImplementedException();
    }
}
