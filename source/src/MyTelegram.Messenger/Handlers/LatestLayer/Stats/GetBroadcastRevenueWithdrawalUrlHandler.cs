//namespace MyTelegram.Messenger.Handlers.LatestLayer.Stats;

/////<summary>
///// Withdraw funds from a channel's <a href="https://corefork.telegram.org/api/revenue">ad revenue balance »</a>.
///// <para>Possible errors</para>
///// Code Type Description
///// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
///// 400 PASSWORD_MISSING You must <a href="https://corefork.telegram.org/api/srp">enable 2FA</a> before executing this operation.
///// 400 PASSWORD_TOO_FRESH_%d The password was modified less than 24 hours ago, try again in %d seconds.
///// See <a href="https://corefork.telegram.org/method/stats.getBroadcastRevenueWithdrawalUrl" />
/////</summary>
//internal sealed class GetBroadcastRevenueWithdrawalUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetBroadcastRevenueWithdrawalUrl, MyTelegram.Schema.Stats.IBroadcastRevenueWithdrawalUrl>
//{
//    protected override Task<MyTelegram.Schema.Stats.IBroadcastRevenueWithdrawalUrl> HandleCoreAsync(IRequestInput input,
//        MyTelegram.Schema.Stats.RequestGetBroadcastRevenueWithdrawalUrl obj)
//    {
//        throw new NotImplementedException();
//    }
//}
