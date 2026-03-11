namespace MyTelegram.Messenger.Handlers.Payments;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 INPUT_STARS_AMOUNT_INVALID  
/// 400 INPUT_STARS_NANOS_INVALID  
/// 400 INVOICE_INVALID The specified invoice is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 RESELL_STARS_TOO_FEW  
/// 400 RESELL_STARS_TOO_MUCH  
/// 400 STARGIFT_OFFER_INVALID  
/// 400 STARGIFT_OFFER_NOT_ALLOWED  
/// 400 STARGIFT_SLUG_INVALID The specified gift slug is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.sendStarGiftOffer"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendStarGiftOfferHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSendStarGiftOffer, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestSendStarGiftOffer obj)
    {
        throw new NotImplementedException();
    }
}