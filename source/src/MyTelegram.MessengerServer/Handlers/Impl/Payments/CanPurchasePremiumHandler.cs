// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

public class CanPurchasePremiumHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCanPurchasePremium, IBool>,
    Payments.ICanPurchasePremiumHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestCanPurchasePremium obj)
    {
        throw new NotImplementedException();
    }
}
