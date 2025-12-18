namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Obtain info about a <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">collectible gift »</a> using a slug obtained from a <a href="https://corefork.telegram.org/api/links#collectible-gift-link">collectible gift link »</a>.
/// Possible errors
/// Code Type Description
/// 400 STARGIFT_SLUG_INVALID The specified gift slug is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getUniqueStarGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetUniqueStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetUniqueStarGift, MyTelegram.Schema.Payments.IUniqueStarGift>
{
    protected override Task<MyTelegram.Schema.Payments.IUniqueStarGift> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetUniqueStarGift obj)
    {
        throw new NotImplementedException();
    }
}