namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getQuickReplies" />
///</summary>
internal sealed class GetQuickRepliesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetQuickReplies, MyTelegram.Schema.Messages.IQuickReplies>
{
    protected override Task<MyTelegram.Schema.Messages.IQuickReplies> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetQuickReplies obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IQuickReplies>(new TQuickReplies
        {
            Chats = new(),
            Messages = new(),
            QuickReplies = new(),
            Users = new()
        });
    }
}
