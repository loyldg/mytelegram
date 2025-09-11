namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.canPurchaseStore" />
///</summary>
internal sealed class CanPurchaseStoreHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCanPurchaseStore, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestCanPurchaseStore obj)
    {
        throw new NotImplementedException();
    }
}
