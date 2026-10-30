
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SetCardStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestSetCardStickers, MyTelegram.Schema.Wallet.ICard>
{
    protected override Task<MyTelegram.Schema.Wallet.ICard> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestSetCardStickers obj)
    {
        throw new NotImplementedException();
    }
}

