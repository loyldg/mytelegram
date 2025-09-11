namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Obtain information about a <a href="https://corefork.telegram.org/api/giveaways">Telegram Premium giftcode »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GIFT_SLUG_EXPIRED The specified gift slug has expired.
/// 400 GIFT_SLUG_INVALID The specified slug is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.checkGiftCode" />
///</summary>
internal sealed class CheckGiftCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCheckGiftCode, MyTelegram.Schema.Payments.ICheckedGiftCode>
{
    protected override Task<MyTelegram.Schema.Payments.ICheckedGiftCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestCheckGiftCode obj)
    {
        throw new NotImplementedException();
    }
}
