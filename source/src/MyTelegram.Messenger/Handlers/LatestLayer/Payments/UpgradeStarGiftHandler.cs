namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Upgrade a <a href="https://corefork.telegram.org/api/gifts">gift</a> to a <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">collectible gift</a>: can only be used if the upgrade was already paid by the gift sender; see <a href="https://corefork.telegram.org/api/gifts#upgrade-a-gift-to-a-collectible-gift">here »</a> for more info on the full flow (including the different flow to use in case the upgrade was not paid by the gift sender).
/// Possible errors
/// Code Type Description
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PAYMENT_REQUIRED Payment is required for this action, see <a href="https://corefork.telegram.org/api/gifts">here »</a> for more info.
/// 400 SAVED_ID_EMPTY The passed inputSavedStarGiftChat.saved_id is empty.
/// 400 STARGIFT_ALREADY_CONVERTED The specified star gift was already converted to Stars.
/// 400 STARGIFT_ALREADY_UPGRADED The specified gift was already upgraded to a collectible gift.
/// 400 STARGIFT_UPGRADE_UNAVAILABLE A received gift can only be upgraded to a collectible gift if the <a href="https://corefork.telegram.org/constructor/messageActionStarGift">messageActionStarGift</a>/<a href="https://corefork.telegram.org/constructor/savedStarGift">savedStarGift</a>.<code>can_upgrade</code> flag is set.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.upgradeStarGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpgradeStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestUpgradeStarGift, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestUpgradeStarGift obj)
    {
        throw new NotImplementedException();
    }
}