// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get unread messages where we were mentioned
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getUnreadMentions" />
///</summary>
internal sealed class GetUnreadMentionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetUnreadMentions, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetUnreadMentionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetUnreadMentions obj)
    {
        throw new NotImplementedException();
    }
}
