
using MyTelegram.Schema.Stats;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stats;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetPollStatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetPollStats, MyTelegram.Schema.Stats.IPollStats>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Stats.IPollStats> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stats.RequestGetPollStats obj)
    {
        return Task.FromResult<IPollStats>(new TPollStats
        {
            VotesGraph = new TStatsGraphError
            {
                Error = "Not implemented"
            }
        });
    }
}

