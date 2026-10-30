
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectCreateSessionHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectCreateSession, MyTelegram.Schema.ITonConnectSession>
{
    protected override Task<MyTelegram.Schema.ITonConnectSession> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectCreateSession obj)
    {
        throw new NotImplementedException();
    }
}

