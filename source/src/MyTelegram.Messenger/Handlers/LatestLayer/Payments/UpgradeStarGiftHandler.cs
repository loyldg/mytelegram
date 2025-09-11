namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.upgradeStarGift" />
///</summary>
internal sealed class UpgradeStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestUpgradeStarGift, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestUpgradeStarGift obj)
    {
        throw new NotImplementedException();
    }
}
