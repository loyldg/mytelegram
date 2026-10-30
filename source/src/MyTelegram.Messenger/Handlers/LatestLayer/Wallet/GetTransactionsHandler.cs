
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetTransactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetTransactions, MyTelegram.Schema.Wallet.ITransactions>
{
    protected override Task<MyTelegram.Schema.Wallet.ITransactions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetTransactions obj)
    {
        throw new NotImplementedException();
    }
}

