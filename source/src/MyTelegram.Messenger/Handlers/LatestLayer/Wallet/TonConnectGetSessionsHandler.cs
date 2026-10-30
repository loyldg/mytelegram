
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectGetSessionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectGetSessions, MyTelegram.Schema.Wallet.ITonConnectSessions>
{
    protected override Task<MyTelegram.Schema.Wallet.ITonConnectSessions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectGetSessions obj)
    {
        throw new NotImplementedException();
    }
}

