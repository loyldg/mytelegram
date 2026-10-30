
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class EnableBackupHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestEnableBackup, MyTelegram.Schema.IWalletState>
{
    protected override Task<MyTelegram.Schema.IWalletState> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestEnableBackup obj)
    {
        throw new NotImplementedException();
    }
}

