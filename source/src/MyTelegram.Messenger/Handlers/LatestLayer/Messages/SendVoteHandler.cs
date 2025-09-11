namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Vote in a <a href="https://corefork.telegram.org/constructor/poll">poll</a>Starting from layer 159, the vote will be sent from the peer specified using <a href="https://corefork.telegram.org/method/messages.saveDefaultSendAs">messages.saveDefaultSendAs</a>.
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
internal sealed class SendVoteHandler(
    ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendVote, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendVote obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);

        var peer = peerHelper.GetPeer(obj.Peer);
        var pollId = await queryProcessor.ProcessAsync(new GetPollIdByMessageIdQuery(peer.PeerId, obj.MsgId));
        if (pollId == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var command = new VoteCommand(PollId.Create(peer.PeerId, pollId!.Value),
            input.ToRequestInfo(),
            input.UserId,
            obj.Options.Select(p =>p).ToList());
        await commandBus.PublishAsync(command);

        return null!;
    }
}
