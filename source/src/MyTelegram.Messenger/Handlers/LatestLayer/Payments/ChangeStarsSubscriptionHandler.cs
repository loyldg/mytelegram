namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Activate or deactivate a <a href="https://corefork.telegram.org/api/invites#paid-invite-links">Telegram Star subscription »</a>.
/// See <a href="https://corefork.telegram.org/method/payments.changeStarsSubscription" />
///</summary>
internal sealed class ChangeStarsSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestChangeStarsSubscription, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestChangeStarsSubscription obj)
    {
        throw new NotImplementedException();
    }
}
