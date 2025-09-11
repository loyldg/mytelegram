namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getStarGiftUpgradePreview" />
///</summary>
internal sealed class GetStarGiftUpgradePreviewHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftUpgradePreview, MyTelegram.Schema.Payments.IStarGiftUpgradePreview>
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftUpgradePreview> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarGiftUpgradePreview obj)
    {
        throw new NotImplementedException();
    }
}
