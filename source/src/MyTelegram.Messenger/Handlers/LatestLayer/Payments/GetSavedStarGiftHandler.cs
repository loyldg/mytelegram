namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Fetch info about specific <a href="https://corefork.telegram.org/api/gifts">gifts</a> owned by a peer we control.Note that unlike what the name suggests, the method can be used to fetch both "saved" and "unsaved" gifts (aka gifts both pinned and not pinned to the profile).
/// Possible errors
/// Code Type Description
/// 400 SAVED_ID_EMPTY The passed inputSavedStarGiftChat.saved_id is empty.
/// 400 STARGIFT_OWNER_INVALID You cannot transfer or sell a gift owned by another user.
/// 400 STARGIFT_SLUG_INVALID The specified gift slug is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getSavedStarGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetSavedStarGift, MyTelegram.Schema.Payments.ISavedStarGifts>
{
    protected override Task<MyTelegram.Schema.Payments.ISavedStarGifts> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetSavedStarGift obj)
    {
        throw new NotImplementedException();
    }
}