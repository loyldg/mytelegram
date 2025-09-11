namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.transferStarGift" />
///</summary>
internal sealed class TransferStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestTransferStarGift, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestTransferStarGift obj)
    {
        throw new NotImplementedException();
    }
}
