namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Pins a received gift on top of the profile of the user or owned channels by using <a href="https://corefork.telegram.org/method/payments.toggleStarGiftsPinnedToTop">payments.toggleStarGiftsPinnedToTop</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.toggleStarGiftsPinnedToTop"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleStarGiftsPinnedToTopHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestToggleStarGiftsPinnedToTop, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestToggleStarGiftsPinnedToTop obj)
    {
        throw new NotImplementedException();
    }
}