// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get messages in a reply thread
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TOPIC_ID_INVALID The specified topic ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getReplies" />
///</summary>
internal sealed class GetRepliesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetReplies, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetRepliesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetReplies obj)
    {
        throw new NotImplementedException();
    }
}
