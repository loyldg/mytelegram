// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Vote in a <a href="https://corefork.telegram.org/constructor/poll">poll</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MESSAGE_POLL_CLOSED Poll closed.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 OPTIONS_TOO_MUCH Too many options provided.
/// 400 OPTION_INVALID Invalid option selected.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 REVOTE_NOT_ALLOWED You cannot change your vote.
/// See <a href="https://corefork.telegram.org/method/messages.sendVote" />
///</summary>
internal sealed class SendVoteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendVote, MyTelegram.Schema.IUpdates>,
    Messages.ISendVoteHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendVote obj)
    {
        throw new NotImplementedException();
    }
}
