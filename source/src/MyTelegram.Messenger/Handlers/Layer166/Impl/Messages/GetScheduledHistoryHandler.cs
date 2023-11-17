// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get scheduled messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getScheduledHistory" />
///</summary>
internal sealed class GetScheduledHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetScheduledHistory, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetScheduledHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetScheduledHistory obj)
    {
        return Task.FromResult<IMessages>(new TMessages
        {
            Chats = new TVector<IChat>(),
            Messages = new TVector<IMessage>(),
            Users = new TVector<IUser>()
        });
    }
}
