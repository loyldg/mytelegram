
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetNftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetNfts, MyTelegram.Schema.Wallet.INftItems>
{
    protected override Task<MyTelegram.Schema.Wallet.INftItems> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetNfts obj)
    {
        throw new NotImplementedException();
    }
}

