namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Display or remove a <a href="https://corefork.telegram.org/api/gifts">received gift »</a> from our profile.
/// See <a href="https://corefork.telegram.org/method/payments.saveStarGift" />
///</summary>
internal sealed class SaveStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSaveStarGift, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestSaveStarGift obj)
    {
        throw new NotImplementedException();
    }
}
