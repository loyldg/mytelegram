namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Re-join a private channel associated to an active <a href="https://corefork.telegram.org/api/invites#paid-invite-links">Telegram Star subscription »</a>.
/// See <a href="https://corefork.telegram.org/method/payments.fulfillStarsSubscription" />
///</summary>
internal sealed class FulfillStarsSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestFulfillStarsSubscription, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestFulfillStarsSubscription obj)
    {
        throw new NotImplementedException();
    }
}
