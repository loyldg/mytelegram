// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get unread reactions to messages you sent
/// See <a href="https://corefork.telegram.org/method/messages.getUnreadReactions" />
///</summary>
internal sealed class GetUnreadReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetUnreadReactions, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetUnreadReactionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetUnreadReactions obj)
    {
        throw new NotImplementedException();
    }
}
