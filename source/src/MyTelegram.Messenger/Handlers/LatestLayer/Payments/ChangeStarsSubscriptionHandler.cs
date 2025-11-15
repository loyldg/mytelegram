namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Activate or deactivate a <a href="https://corefork.telegram.org/api/invites#paid-invite-links">Telegram Star subscription »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.changeStarsSubscription"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ChangeStarsSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestChangeStarsSubscription, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestChangeStarsSubscription obj)
    {
        throw new NotImplementedException();
    }
}