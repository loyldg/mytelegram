namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getUniqueStarGift" />
///</summary>
internal sealed class GetUniqueStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetUniqueStarGift, MyTelegram.Schema.Payments.IUniqueStarGift>
{
    protected override Task<MyTelegram.Schema.Payments.IUniqueStarGift> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetUniqueStarGift obj)
    {
        throw new NotImplementedException();
    }
}
