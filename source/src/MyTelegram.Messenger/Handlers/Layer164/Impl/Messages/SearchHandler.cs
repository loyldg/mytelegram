// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns found messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 FROM_PEER_INVALID The specified from_id is invalid.
/// 400 INPUT_FILTER_INVALID The specified filter is invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PEER_ID_NOT_SUPPORTED The provided peer ID is not supported.
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.search" />
///</summary>
internal sealed class SearchHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearch, MyTelegram.Schema.Messages.IMessages>,
    Messages.ISearchHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearch obj)
    {
        throw new NotImplementedException();
    }
}
