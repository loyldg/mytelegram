namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Fetch a list of <a href="https://corefork.telegram.org/api/giveaways#star-giveaways">star giveaway options »</a>.
/// See <a href="https://corefork.telegram.org/method/payments.getStarsGiveawayOptions" />
///</summary>
internal sealed class GetStarsGiveawayOptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsGiveawayOptions, TVector<MyTelegram.Schema.IStarsGiveawayOption>>
{
    protected override Task<TVector<MyTelegram.Schema.IStarsGiveawayOption>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarsGiveawayOptions obj)
    {
        throw new NotImplementedException();
    }
}
