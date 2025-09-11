namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get poll results for non-anonymous polls
/// <para>Possible errors</para>
/// Code Type Description
/// 403 BROADCAST_FORBIDDEN Channel poll voters and reactions cannot be fetched to prevent deanonymization.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 403 POLL_VOTE_REQUIRED Cast a vote in the poll before calling this method.
/// See <a href="https://corefork.telegram.org/method/messages.getPollVotes" />
///</summary>
internal sealed class GetPollVotesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPollVotes, MyTelegram.Schema.Messages.IVotesList>
{
    protected override Task<MyTelegram.Schema.Messages.IVotesList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPollVotes obj)
    {
        throw new NotImplementedException();
    }
}
