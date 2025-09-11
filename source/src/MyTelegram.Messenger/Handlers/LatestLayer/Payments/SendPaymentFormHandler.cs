namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Send compiled payment form
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.sendPaymentForm" />
///</summary>
internal sealed class SendPaymentFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSendPaymentForm, MyTelegram.Schema.Payments.IPaymentResult>
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestSendPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}
