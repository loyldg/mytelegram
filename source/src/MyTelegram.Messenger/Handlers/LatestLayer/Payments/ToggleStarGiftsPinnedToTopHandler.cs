namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.toggleStarGiftsPinnedToTop" />
///</summary>
internal sealed class ToggleStarGiftsPinnedToTopHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestToggleStarGiftsPinnedToTop, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestToggleStarGiftsPinnedToTop obj)
    {
        throw new NotImplementedException();
    }
}
