namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Obtain a list of <a href="https://corefork.telegram.org/api/stars#buying-or-gifting-stars">Telegram Stars topup options »</a> as <a href="https://corefork.telegram.org/constructor/starsTopupOption">starsTopupOption</a> constructors.
/// See <a href="https://corefork.telegram.org/method/payments.getStarsTopupOptions" />
///</summary>
internal sealed class GetStarsTopupOptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsTopupOptions, TVector<MyTelegram.Schema.IStarsTopupOption>>
{
    protected override Task<TVector<MyTelegram.Schema.IStarsTopupOption>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarsTopupOptions obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.IStarsTopupOption>>([]);
    }
}
