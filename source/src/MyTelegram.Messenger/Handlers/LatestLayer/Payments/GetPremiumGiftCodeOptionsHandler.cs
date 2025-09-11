namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Obtain a list of Telegram Premium <a href="https://corefork.telegram.org/api/giveaways">giveaway/gift code »</a> options.
/// See <a href="https://corefork.telegram.org/method/payments.getPremiumGiftCodeOptions" />
///</summary>
internal sealed class GetPremiumGiftCodeOptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetPremiumGiftCodeOptions, TVector<MyTelegram.Schema.IPremiumGiftCodeOption>>
{
    protected override Task<TVector<MyTelegram.Schema.IPremiumGiftCodeOption>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetPremiumGiftCodeOptions obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.IPremiumGiftCodeOption>>(
            [
                new TPremiumGiftCodeOption
                {
                    Months=3,
                    Currency="USD",
                    Users=1
                }
            ]);
    }
}
