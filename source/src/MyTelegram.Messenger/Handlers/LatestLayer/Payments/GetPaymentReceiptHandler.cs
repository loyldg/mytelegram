namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Get payment receipt
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getPaymentReceipt"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPaymentReceiptHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetPaymentReceipt, MyTelegram.Schema.Payments.IPaymentReceipt>
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentReceipt> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetPaymentReceipt obj)
    {
        throw new NotImplementedException();
    }
}