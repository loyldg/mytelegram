
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectSubmitResponseHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectSubmitResponse, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectSubmitResponse obj)
    {
        throw new NotImplementedException();
    }
}

