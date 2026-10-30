
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TonConnectNextEventIdHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestTonConnectNextEventId, MyTelegram.Schema.ITonConnectNextEventId>
{
    protected override Task<MyTelegram.Schema.ITonConnectNextEventId> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestTonConnectNextEventId obj)
    {
        throw new NotImplementedException();
    }
}

