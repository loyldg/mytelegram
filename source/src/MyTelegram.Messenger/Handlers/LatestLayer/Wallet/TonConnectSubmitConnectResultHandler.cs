
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectSubmitConnectResultHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectSubmitConnectResult, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectSubmitConnectResult obj)
    {
        throw new NotImplementedException();
    }
}

