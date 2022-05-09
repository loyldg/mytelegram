using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetPollVotesHandler : RpcResultObjectHandler<RequestGetPollVotes, IVotesList>,
    IGetPollVotesHandler
{
    protected override Task<IVotesList> HandleCoreAsync(IRequestInput input,
        RequestGetPollVotes obj)
    {
        throw new NotImplementedException();
    }
}
