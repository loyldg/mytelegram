// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns the list of messages by their IDs.
/// See <a href="https://corefork.telegram.org/method/messages.getMessages" />
///</summary>
internal sealed class GetMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessages, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetMessagesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessages obj)
    {
        throw new NotImplementedException();
    }
}
