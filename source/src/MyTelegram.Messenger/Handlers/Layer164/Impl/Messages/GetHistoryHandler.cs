// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns the conversation history with one interlocutor / within a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getHistory" />
///</summary>
internal sealed class GetHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetHistory, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetHistory obj)
    {
        throw new NotImplementedException();
    }
}
