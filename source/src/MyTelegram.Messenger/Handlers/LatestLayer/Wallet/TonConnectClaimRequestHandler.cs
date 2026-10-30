
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectClaimRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectClaimRequest, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectClaimRequest obj)
    {
        throw new NotImplementedException();
    }
}

