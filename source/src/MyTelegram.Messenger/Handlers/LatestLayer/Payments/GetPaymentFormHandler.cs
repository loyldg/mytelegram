namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Get a payment form
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOOST_PEER_INVALID The specified <code>boost_peer</code> is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SLUG_INVALID The specified invoice slug is invalid.
/// 400 UNTIL_DATE_INVALID Invalid until date provided.
/// See <a href="https://corefork.telegram.org/method/payments.getPaymentForm" />
///</summary>
internal sealed class GetPaymentFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetPaymentForm, MyTelegram.Schema.Payments.IPaymentForm>
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentForm> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}
