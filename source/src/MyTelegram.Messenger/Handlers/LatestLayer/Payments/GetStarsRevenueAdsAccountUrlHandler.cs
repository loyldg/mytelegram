namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Returns a URL for a Telegram Ad platform account that can be used to set up advertisements for channel/bot in <code>peer</code>, paid using the Telegram Stars owned by the specified <code>peer</code>, see <a href="https://corefork.telegram.org/api/stars#paying-for-ads">here »</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.getStarsRevenueAdsAccountUrl" />
///</summary>
internal sealed class GetStarsRevenueAdsAccountUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsRevenueAdsAccountUrl, MyTelegram.Schema.Payments.IStarsRevenueAdsAccountUrl>
{
    protected override Task<MyTelegram.Schema.Payments.IStarsRevenueAdsAccountUrl> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarsRevenueAdsAccountUrl obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.IStarsRevenueAdsAccountUrl>(
            new MyTelegram.Schema.Payments.TStarsRevenueAdsAccountUrl
            {
                Url = "https://ads.telegram.org/"
            });
    }
}
