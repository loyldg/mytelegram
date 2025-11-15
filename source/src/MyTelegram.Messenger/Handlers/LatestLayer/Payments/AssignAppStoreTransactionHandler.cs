namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Informs server about a purchase made through the App Store: for official applications only.
/// Possible errors
/// Code Type Description
/// 400 INPUT_PURPOSE_INVALID The specified payment purpose is invalid.
/// 400 RECEIPT_EMPTY The specified receipt is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.assignAppStoreTransaction"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class AssignAppStoreTransactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestAssignAppStoreTransaction, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestAssignAppStoreTransaction obj)
    {
        throw new NotImplementedException();
    }
}