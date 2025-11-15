namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Re-join a private channel associated to an active <a href="https://corefork.telegram.org/api/invites#paid-invite-links">Telegram Star subscription »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.fulfillStarsSubscription"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class FulfillStarsSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestFulfillStarsSubscription, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestFulfillStarsSubscription obj)
    {
        throw new NotImplementedException();
    }
}