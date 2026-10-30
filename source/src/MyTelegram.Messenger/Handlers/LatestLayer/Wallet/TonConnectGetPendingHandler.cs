
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectGetPendingHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectGetPending, MyTelegram.Schema.Wallet.ITonConnectPending>
{
    protected override Task<MyTelegram.Schema.Wallet.ITonConnectPending> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectGetPending obj)
    {
        throw new NotImplementedException();
    }
}

