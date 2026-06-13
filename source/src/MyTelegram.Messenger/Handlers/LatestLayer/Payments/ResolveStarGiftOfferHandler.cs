namespace MyTelegram.Messenger.Handlers.Payments;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.resolveStarGiftOffer"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ResolveStarGiftOfferHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestResolveStarGiftOffer, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestResolveStarGiftOffer obj)
    {
        throw new NotImplementedException();
    }
}