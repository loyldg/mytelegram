// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Get a payment form
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.getPaymentForm" />
///</summary>
internal sealed class GetPaymentFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetPaymentForm, MyTelegram.Schema.Payments.IPaymentForm>,
    Payments.IGetPaymentFormHandler
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentForm> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}
