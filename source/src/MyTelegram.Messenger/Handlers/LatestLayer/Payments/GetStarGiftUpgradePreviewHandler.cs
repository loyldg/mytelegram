namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Obtain a preview of the possible attributes (chosen randomly) a <a href="https://corefork.telegram.org/api/gifts">gift »</a> can receive after upgrading it to a <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">collectible gift »</a>, see <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 STARGIFT_INVALID The passed gift is invalid.
/// 400 STARGIFT_UPGRADE_UNAVAILABLE A received gift can only be upgraded to a collectible gift if the <a href="https://corefork.telegram.org/constructor/messageActionStarGift">messageActionStarGift</a>/<a href="https://corefork.telegram.org/constructor/savedStarGift">savedStarGift</a>.<code>can_upgrade</code> flag is set.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getStarGiftUpgradePreview"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStarGiftUpgradePreviewHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftUpgradePreview, MyTelegram.Schema.Payments.IStarGiftUpgradePreview>
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftUpgradePreview> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftUpgradePreview obj)
    {
        throw new NotImplementedException();
    }
}