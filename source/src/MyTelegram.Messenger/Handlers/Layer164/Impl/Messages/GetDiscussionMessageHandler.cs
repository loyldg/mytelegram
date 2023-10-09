// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/threads">discussion message</a> from the <a href="https://corefork.telegram.org/api/discussion">associated discussion group</a> of a channel to show it on top of the comment section, without actually joining the group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TOPIC_ID_INVALID The specified topic ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDiscussionMessage" />
///</summary>
internal sealed class GetDiscussionMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDiscussionMessage, MyTelegram.Schema.Messages.IDiscussionMessage>,
    Messages.IGetDiscussionMessageHandler
{
    protected override Task<MyTelegram.Schema.Messages.IDiscussionMessage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDiscussionMessage obj)
    {
        throw new NotImplementedException();
    }
}
