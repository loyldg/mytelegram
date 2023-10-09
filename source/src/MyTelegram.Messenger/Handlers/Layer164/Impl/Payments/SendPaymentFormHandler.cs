// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Send compiled payment form
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.sendPaymentForm" />
///</summary>
internal sealed class SendPaymentFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSendPaymentForm, MyTelegram.Schema.Payments.IPaymentResult>,
    Payments.ISendPaymentFormHandler
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestSendPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}
