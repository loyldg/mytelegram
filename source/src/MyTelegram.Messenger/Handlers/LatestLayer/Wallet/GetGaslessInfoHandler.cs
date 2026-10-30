
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetGaslessInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetGaslessInfo, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetGaslessInfo obj)
    {
        throw new NotImplementedException();
    }
}

