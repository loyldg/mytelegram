
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetStateHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetState, MyTelegram.Schema.IWalletState>
{
    protected override Task<MyTelegram.Schema.IWalletState> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetState obj)
    {
        return Task.FromResult<MyTelegram.Schema.IWalletState>(new MyTelegram.Schema.TWalletState
        {
            Address = string.Empty,
            BackupEnabled = false,
            Balance = 0,
            CanEnableBackup = false,
            CanExportPhrase = false,
            PublicKey = Array.Empty<byte>()
        });
    }
}

