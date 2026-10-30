
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectRegisterKeyHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectRegisterKey, MyTelegram.Schema.Wallet.ITonConnectChallenge>
{
    protected override Task<MyTelegram.Schema.Wallet.ITonConnectChallenge> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectRegisterKey obj)
    {
        throw new NotImplementedException();
    }
}

