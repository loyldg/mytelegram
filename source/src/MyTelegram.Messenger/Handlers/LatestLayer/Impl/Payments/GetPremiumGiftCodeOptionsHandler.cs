// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getPremiumGiftCodeOptions" />
///</summary>
internal sealed class GetPremiumGiftCodeOptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetPremiumGiftCodeOptions, TVector<MyTelegram.Schema.IPremiumGiftCodeOption>>,
    Payments.IGetPremiumGiftCodeOptionsHandler
{
    protected override Task<TVector<MyTelegram.Schema.IPremiumGiftCodeOption>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetPremiumGiftCodeOptions obj)
    {
        throw new NotImplementedException();
    }
}
