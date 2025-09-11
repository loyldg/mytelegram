//using MyTelegram.Schema.Stats;

//namespace MyTelegram.Messenger.Handlers.LatestLayer.Stats;

/////<summary>
///// Fetch <a href="https://corefork.telegram.org/api/revenue">channel ad revenue transaction history »</a>.
///// <para>Possible errors</para>
///// Code Type Description
///// 400 CHANNEL_INVALID The provided channel is invalid.
///// See <a href="https://corefork.telegram.org/method/stats.getBroadcastRevenueTransactions" />
/////</summary>
//internal sealed class GetBroadcastRevenueTransactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetBroadcastRevenueTransactions, MyTelegram.Schema.Stats.IBroadcastRevenueTransactions>
//{
//    protected override Task<MyTelegram.Schema.Stats.IBroadcastRevenueTransactions> HandleCoreAsync(IRequestInput input,
//        MyTelegram.Schema.Stats.RequestGetBroadcastRevenueTransactions obj)
//    {
//        return Task.FromResult<MyTelegram.Schema.Stats.IBroadcastRevenueTransactions>(new TBroadcastRevenueTransactions
//        {
//            Transactions = []
//        });
//    }
//}
