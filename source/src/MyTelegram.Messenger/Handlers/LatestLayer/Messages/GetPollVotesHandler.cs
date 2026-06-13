namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get poll results for non-anonymous polls
/// Possible errors
/// Code Type Description
/// 403 BROADCAST_FORBIDDEN Channel poll voters and reactions cannot be fetched to prevent deanonymization.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 403 POLL_VOTE_REQUIRED Cast a vote in the poll before calling this method.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getPollVotes"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPollVotesHandler(IQueryProcessor queryProcessor, IChatConverterService chatConverterService, IUserConverterService userConverterService, IChannelAppService channelAppService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPollVotes, MyTelegram.Schema.Messages.IVotesList>
{
    protected override async Task<MyTelegram.Schema.Messages.IVotesList> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetPollVotes obj)
    {
        var peer = obj.Peer.ToPeer();
        var ownerPeerId = peer.PeerId;
        if (peer.PeerType == PeerType.Channel)
        {
            var channelReadModel = await channelAppService.GetAsync(peer.PeerId);
            if (channelReadModel.Broadcast)
            {
                RpcErrors.RpcErrors403.BroadcastForbidden.ThrowRpcError();
            }
        }

        var messageReadModel = await queryProcessor.ProcessAsync(new GetMessageByPeerIdAndMessageIdQuery(ownerPeerId, obj.Id));
        if (messageReadModel == null || messageReadModel.PollId == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var limit = obj.Limit;
        if (limit < 0 || limit > 500)
        {
            limit = 100;
        }

        int.TryParse(obj.Offset, out var offset);
        var pollVoterReadModels = await queryProcessor.ProcessAsync(new GetPollVotesQuery(messageReadModel!.PollId!.Value, obj.Option, offset, limit));
        string? nextOffset = null;
        if (pollVoterReadModels.Count == limit)
        {
            nextOffset = (offset + pollVoterReadModels.Count).ToString();
        }

        var result = new TVotesList
        {
            Count = pollVoterReadModels.Count,
            NextOffset = nextOffset,
            Chats = [],
            Users = [],
            Votes = []
        };
        var userIds = new List<long>();
        foreach (var item in pollVoterReadModels)
        {
            var messagePeerVote = new TMessagePeerVote
            {
                Date = item.Date,
                Option = item.Option,
                Peer = new TPeerUser
                {
                    UserId = item.VoterPeerId
                }
            };
            if (messagePeerVote.Date == 0)
            {
                messagePeerVote.Date = DateTime.UtcNow.ToTimestamp();
            }

            userIds.Add(item.VoterPeerId);
            result.Votes.Add(messagePeerVote);
        }

        if (peer.PeerType == PeerType.Channel)
        {
            var channel = await chatConverterService.GetChannelAsync(input, peer.PeerId, true, null, layer: input.Layer);
            result.Chats.Add(channel);
        }

        var users = await userConverterService.GetUserListAsync(input, userIds, false, false, input.Layer);
        result.Users.AddRange(users);
        return result;
    }
}