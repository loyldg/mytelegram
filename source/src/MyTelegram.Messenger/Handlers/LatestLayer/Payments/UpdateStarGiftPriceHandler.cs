namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// A <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">collectible gift we own »</a> can be put up for sale on the <a href="https://telegram.org/blog/gift-marketplace-and-more">gift marketplace »</a> with this method, see <a href="https://corefork.telegram.org/api/gifts#reselling-collectible-gifts">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 SAVED_ID_EMPTY The passed inputSavedStarGiftChat.saved_id is empty.
/// 400 STARGIFT_NOT_FOUND The specified gift was not found.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.updateStarGiftPrice"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateStarGiftPriceHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestUpdateStarGiftPrice, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestUpdateStarGiftPrice obj)
    {
        return Task.FromResult<MyTelegram.Schema.IUpdates>(new TUpdates { Chats = [], Updates = [], Users = [], Date = CurrentDate });
    }
}