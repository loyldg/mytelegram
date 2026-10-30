
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetExistingWaltBalanceHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetExistingWaltBalance, MyTelegram.Schema.Wallet.IExistingBalance>
{
    protected override Task<MyTelegram.Schema.Wallet.IExistingBalance> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetExistingWaltBalance obj)
    {
        return Task.FromResult<MyTelegram.Schema.Wallet.IExistingBalance>(new MyTelegram.Schema.Wallet.TExistingBalance
        {
            HasBalance = false,
            Url = string.Empty
        });
    }
}

