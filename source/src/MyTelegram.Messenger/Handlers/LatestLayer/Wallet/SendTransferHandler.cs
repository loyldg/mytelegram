
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SendTransferHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestSendTransfer, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestSendTransfer obj)
    {
        throw new NotImplementedException();
    }
}

