// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Deletes communication history.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_REVOKE_DATE_UNSUPPORTED <code>min_date</code> and <code>max_date</code> are not available for using with non-user peers.
/// 400 MAX_DATE_INVALID The specified maximum date is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MIN_DATE_INVALID The specified minimum date is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteHistory" />
///</summary>
internal sealed class DeleteHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Messages.IDeleteHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteHistory obj)
    {
        throw new NotImplementedException();
    }
}
