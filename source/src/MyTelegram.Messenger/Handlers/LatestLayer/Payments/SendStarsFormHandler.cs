namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Make a payment using <a href="https://corefork.telegram.org/api/stars#using-stars">Telegram Stars, see here »</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BALANCE_TOO_LOW The transaction cannot be completed because the current <a href="https://corefork.telegram.org/api/stars">Telegram Stars balance</a> is too low.
/// 400 FORM_EXPIRED The form was generated more than 10 minutes ago and has expired, please re-generate it using <a href="https://corefork.telegram.org/method/payments.getPaymentForm">payments.getPaymentForm</a> and pass the new <code>form_id</code>.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.sendStarsForm" />
///</summary>
internal sealed class SendStarsFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSendStarsForm, MyTelegram.Schema.Payments.IPaymentResult>
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestSendStarsForm obj)
    {
        throw new NotImplementedException();
    }
}
