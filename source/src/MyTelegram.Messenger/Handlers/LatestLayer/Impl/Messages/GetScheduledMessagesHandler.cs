// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get scheduled messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getScheduledMessages" />
///</summary>
internal sealed class GetScheduledMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetScheduledMessages, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetScheduledMessagesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetScheduledMessages obj)
    {
        throw new NotImplementedException();
    }
}
