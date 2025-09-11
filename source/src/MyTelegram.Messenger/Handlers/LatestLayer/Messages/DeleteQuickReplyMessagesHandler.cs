namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.deleteQuickReplyMessages" />
///</summary>
internal sealed class DeleteQuickReplyMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteQuickReplyMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteQuickReplyMessages obj)
    {
        throw new NotImplementedException();
    }
}
