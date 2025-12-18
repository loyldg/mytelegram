namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Obtain info about <a href="https://corefork.telegram.org/api/stars#balance-and-transaction-history">Telegram Star transactions »</a> using specific transaction IDs.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TRANSACTION_ID_INVALID The specified transaction ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getStarsTransactionsByID"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStarsTransactionsByIDHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsTransactionsByID, MyTelegram.Schema.Payments.IStarsStatus>
{
    protected override Task<MyTelegram.Schema.Payments.IStarsStatus> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarsTransactionsByID obj)
    {
        throw new NotImplementedException();
    }
}