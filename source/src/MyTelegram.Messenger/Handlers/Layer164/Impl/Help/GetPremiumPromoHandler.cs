// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get Telegram Premium promotion information
/// See <a href="https://corefork.telegram.org/method/help.getPremiumPromo" />
///</summary>
internal sealed class GetPremiumPromoHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPremiumPromo, MyTelegram.Schema.Help.IPremiumPromo>,
    Help.IGetPremiumPromoHandler
{
    protected override Task<MyTelegram.Schema.Help.IPremiumPromo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetPremiumPromo obj)
    {
        throw new NotImplementedException();
    }
}
