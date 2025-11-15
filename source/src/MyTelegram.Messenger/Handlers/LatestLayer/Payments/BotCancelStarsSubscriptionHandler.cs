namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Cancel a <a href="https://corefork.telegram.org/api/subscriptions#bot-subscriptions">bot subscription</a>
/// Possible errors
/// Code Type Description
/// 400 CHARGE_ID_INVALID The specified charge_id is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.botCancelStarsSubscription"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class BotCancelStarsSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestBotCancelStarsSubscription, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestBotCancelStarsSubscription obj)
    {
        throw new NotImplementedException();
    }
}