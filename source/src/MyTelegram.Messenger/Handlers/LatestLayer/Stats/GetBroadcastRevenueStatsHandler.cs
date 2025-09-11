//using MyTelegram.Schema.Stats;

//namespace MyTelegram.Messenger.Handlers.LatestLayer.Stats;

/////<summary>
///// Get <a href="https://corefork.telegram.org/api/revenue">channel ad revenue statistics »</a>.
///// <para>Possible errors</para>
///// Code Type Description
///// 400 CHANNEL_INVALID The provided channel is invalid.
///// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
///// See <a href="https://corefork.telegram.org/method/stats.getBroadcastRevenueStats" />
/////</summary>
//internal sealed class GetBroadcastRevenueStatsHandler(ILayeredService<IBroadcastRevenueBalancesResponseConverter> broadcastRevenueBalancesLayeredService) : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetBroadcastRevenueStats, MyTelegram.Schema.Stats.IBroadcastRevenueStats>
//{
//    protected override Task<MyTelegram.Schema.Stats.IBroadcastRevenueStats> HandleCoreAsync(IRequestInput input,
//        MyTelegram.Schema.Stats.RequestGetBroadcastRevenueStats obj)
//    {
//        return Task.FromResult<MyTelegram.Schema.Stats.IBroadcastRevenueStats>(new TBroadcastRevenueStats
//        {
//            Balances = broadcastRevenueBalancesLayeredService.GetConverter(input.Layer).ToLayeredData(new TBroadcastRevenueBalances
//            {

//            }),
//            TopHoursGraph = new TStatsGraph
//            {
//                Json = new TDataJSON
//                {
//                    Data = string.Empty
//                }
//            },
//            RevenueGraph = new TStatsGraph
//            {
//                Json = new TDataJSON
//                {
//                    Data = string.Empty
//                }
//            },
//            UsdRate = 0
//        });
//    }
//}
