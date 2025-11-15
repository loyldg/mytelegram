namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Convert a <a href="https://corefork.telegram.org/api/gifts">received gift »</a> into Telegram Stars: this will permanently destroy the gift, converting it into <a href="https://corefork.telegram.org/constructor/starGift">starGift</a>.<code>convert_stars</code> <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a>, added to the user's balance.Note that <a href="https://corefork.telegram.org/constructor/starGift">starGift</a>.<code>convert_stars</code> will be less than the buying price (<a href="https://corefork.telegram.org/constructor/starGift">starGift</a>.<code>stars</code>) of the gift if it was originally bought using Telegram Stars bought a long time ago.
/// Possible errors
/// Code Type Description
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 SAVED_ID_EMPTY The passed inputSavedStarGiftChat.saved_id is empty.
/// 400 STARGIFT_PEER_INVALID The specified inputSavedStarGiftChat.peer is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.convertStarGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ConvertStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestConvertStarGift, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestConvertStarGift obj)
    {
        throw new NotImplementedException();
    }
}