namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Display or remove a <a href="https://corefork.telegram.org/api/gifts">received gift »</a> from our profile.
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 SAVED_ID_EMPTY The passed inputSavedStarGiftChat.saved_id is empty.
/// 400 STARGIFT_OWNER_INVALID You cannot transfer or sell a gift owned by another user.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.saveStarGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSaveStarGift, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestSaveStarGift obj)
    {
        throw new NotImplementedException();
    }
}