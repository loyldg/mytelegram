namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Send compiled payment form
/// Possible errors
/// Code Type Description
/// 400 FORM_UNSUPPORTED Please update your client.
/// 400 INVOICE_INVALID The specified invoice is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PAYMENT_CREDENTIALS_INVALID The specified payment credentials are invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TMP_PASSWORD_INVALID The passed tmp_password is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.sendPaymentForm"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class SendPaymentFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSendPaymentForm, MyTelegram.Schema.Payments.IPaymentResult>
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestSendPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}