using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/stars">Telegram Star revenue statistics »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.getStarsRevenueStats" />
///</summary>
internal sealed class GetStarsRevenueStatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsRevenueStats, MyTelegram.Schema.Payments.IStarsRevenueStats>
{
    protected override Task<MyTelegram.Schema.Payments.IStarsRevenueStats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarsRevenueStats obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.IStarsRevenueStats>(new TStarsRevenueStats
        {
            Status = new TStarsRevenueStatus
            {
                AvailableBalance = new TStarsAmount
                {
                    Amount = 10000
                },
                CurrentBalance = new TStarsAmount
                {
                    Amount = 10000
                },
                OverallRevenue = new TStarsAmount
                {
                    Amount = 10000
                },
                WithdrawalEnabled = false
            },
            RevenueGraph = new TStatsGraphError
            {
                Error = "Not implemented"
            },
            UsdRate = 1
        });
    }
}
