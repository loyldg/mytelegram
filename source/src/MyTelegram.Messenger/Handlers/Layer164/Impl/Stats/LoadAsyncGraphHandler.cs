// ReSharper disable All

namespace MyTelegram.Handlers.Stats;

///<summary>
/// Load <a href="https://corefork.telegram.org/api/stats">channel statistics graph</a> asynchronously
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GRAPH_EXPIRED_RELOAD This graph has expired, please obtain a new graph token.
/// 400 GRAPH_INVALID_RELOAD Invalid graph token provided, please reload the stats and provide the updated token.
/// 400 GRAPH_OUTDATED_RELOAD The graph is outdated, please get a new async token using stats.getBroadcastStats.
/// See <a href="https://corefork.telegram.org/method/stats.loadAsyncGraph" />
///</summary>
internal sealed class LoadAsyncGraphHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestLoadAsyncGraph, MyTelegram.Schema.IStatsGraph>,
    Stats.ILoadAsyncGraphHandler
{
    protected override Task<MyTelegram.Schema.IStatsGraph> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stats.RequestLoadAsyncGraph obj)
    {
        throw new NotImplementedException();
    }
}
