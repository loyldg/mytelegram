// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Mark mentions as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readMentions" />
///</summary>
internal sealed class ReadMentionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadMentions, MyTelegram.Schema.Messages.IAffectedHistory>,
    Messages.IReadMentionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadMentions obj)
    {
        throw new NotImplementedException();
    }
}
