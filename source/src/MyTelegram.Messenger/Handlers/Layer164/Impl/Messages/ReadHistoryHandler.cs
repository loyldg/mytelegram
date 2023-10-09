// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Marks message history as read.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readHistory" />
///</summary>
internal sealed class ReadHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadHistory, MyTelegram.Schema.Messages.IAffectedMessages>,
    Messages.IReadHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadHistory obj)
    {
        throw new NotImplementedException();
    }
}
