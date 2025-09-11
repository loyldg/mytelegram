namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Obtain a list of <a href="https://corefork.telegram.org/api/stars#buying-or-gifting-stars">Telegram Stars gift options »</a> as <a href="https://corefork.telegram.org/constructor/starsGiftOption">starsGiftOption</a> constructors.
/// See <a href="https://corefork.telegram.org/method/payments.getStarsGiftOptions" />
///</summary>
internal sealed class GetStarsGiftOptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsGiftOptions, TVector<MyTelegram.Schema.IStarsGiftOption>>
{
    protected override Task<TVector<MyTelegram.Schema.IStarsGiftOption>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarsGiftOptions obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.IStarsGiftOption>>([]);
    }
}
