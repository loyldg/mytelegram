// ReSharper disable All

namespace MyTelegram.Handlers.Stats;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stats.getStoryStats" />
///</summary>
internal sealed class GetStoryStatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetStoryStats, MyTelegram.Schema.Stats.IStoryStats>,
    Stats.IGetStoryStatsHandler
{
    protected override Task<MyTelegram.Schema.Stats.IStoryStats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stats.RequestGetStoryStats obj)
    {
        throw new NotImplementedException();
    }
}
