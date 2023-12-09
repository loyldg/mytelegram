// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get poll results for non-anonymous polls
/// <para>Possible errors</para>
/// Code Type Description
/// 403 BROADCAST_FORBIDDEN Participants of polls in channels should stay anonymous.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 403 POLL_VOTE_REQUIRED Cast a vote in the poll before calling this method.
/// See <a href="https://corefork.telegram.org/method/messages.getPollVotes" />
///</summary>
internal sealed class GetPollVotesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPollVotes, MyTelegram.Schema.Messages.IVotesList>,
    Messages.IGetPollVotesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IVotesList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPollVotes obj)
    {
        throw new NotImplementedException();
    }
}
