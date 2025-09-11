namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.botCancelStarsSubscription" />
///</summary>
internal sealed class BotCancelStarsSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestBotCancelStarsSubscription, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestBotCancelStarsSubscription obj)
    {
        throw new NotImplementedException();
    }
}
