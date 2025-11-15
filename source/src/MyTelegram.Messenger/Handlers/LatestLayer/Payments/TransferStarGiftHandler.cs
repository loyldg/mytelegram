namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Transfer a <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">collectible gift</a> to another user or channel: can only be used if transfer is free (i.e. <a href="https://corefork.telegram.org/constructor/messageActionStarGiftUnique">messageActionStarGiftUnique</a>.<code>transfer_stars</code> is not set); see <a href="https://corefork.telegram.org/api/gifts#transferring-collectible-gifts">here »</a> for more info on the full flow (including the different flow to use in case the transfer isn't free).
/// Possible errors
/// Code Type Description
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PAYMENT_REQUIRED Payment is required for this action, see <a href="https://corefork.telegram.org/api/gifts">here »</a> for more info.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SAVED_ID_EMPTY The passed inputSavedStarGiftChat.saved_id is empty.
/// 400 STARGIFT_NOT_FOUND The specified gift was not found.
/// 400 STARGIFT_OWNER_INVALID You cannot transfer or sell a gift owned by another user.
/// 400 STARGIFT_PEER_INVALID The specified inputSavedStarGiftChat.peer is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.transferStarGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class TransferStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestTransferStarGift, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestTransferStarGift obj)
    {
        throw new NotImplementedException();
    }
}