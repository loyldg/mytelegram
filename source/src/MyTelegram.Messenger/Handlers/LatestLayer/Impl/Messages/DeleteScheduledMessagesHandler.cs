// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Delete scheduled messages
/// See <a href="https://corefork.telegram.org/method/messages.deleteScheduledMessages" />
///</summary>
internal sealed class DeleteScheduledMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteScheduledMessages, MyTelegram.Schema.IUpdates>,
    Messages.IDeleteScheduledMessagesHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteScheduledMessages obj)
    {
        throw new NotImplementedException();
    }
}
