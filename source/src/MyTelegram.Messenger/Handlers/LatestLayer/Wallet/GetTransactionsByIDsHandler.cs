
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetTransactionsByIDsHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetTransactionsByIDs, MyTelegram.Schema.Wallet.ITransactions>
{
    protected override Task<MyTelegram.Schema.Wallet.ITransactions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetTransactionsByIDs obj)
    {
        throw new NotImplementedException();
    }
}

