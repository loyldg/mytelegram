
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectCloseSessionHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectCloseSession, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectCloseSession obj)
    {
        throw new NotImplementedException();
    }
}

