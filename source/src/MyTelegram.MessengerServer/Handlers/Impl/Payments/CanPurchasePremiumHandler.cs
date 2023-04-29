// ReSharper disable All

using MyTelegram.Schema.Payments;

namespace MyTelegram.Handlers.Payments;

public class CanPurchasePremiumHandler : RpcResultObjectHandler<RequestCanPurchasePremium, IBool>,
    Payments.ICanPurchasePremiumHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCanPurchasePremium obj)
    {
        throw new NotImplementedException();
    }
}
