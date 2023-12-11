// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Get payment receipt
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.getPaymentReceipt" />
///</summary>
internal sealed class GetPaymentReceiptHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetPaymentReceipt, MyTelegram.Schema.Payments.IPaymentReceipt>,
    Payments.IGetPaymentReceiptHandler
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentReceipt> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetPaymentReceipt obj)
    {
        throw new NotImplementedException();
    }
}
