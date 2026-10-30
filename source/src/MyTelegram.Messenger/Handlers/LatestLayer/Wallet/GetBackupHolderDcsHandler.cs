
using MyTelegram.Schema.Wallet;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetBackupHolderDcsHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetBackupHolderDcs, TVector<MyTelegram.Schema.Wallet.IHolderDc>>
{
    protected override Task<TVector<IHolderDc>> HandleCoreAsync(IRequestInput request, RequestGetBackupHolderDcs obj)
    {
        throw new NotImplementedException();
    }
}

