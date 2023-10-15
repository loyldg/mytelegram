// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get Telegram Premium promotion information
/// See <a href="https://corefork.telegram.org/method/help.getPremiumPromo" />
///</summary>
internal sealed class GetPremiumPromoHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPremiumPromo, MyTelegram.Schema.Help.IPremiumPromo>,
    Help.IGetPremiumPromoHandler
{
    private readonly ILayeredService<IPremiumPromoConverter> _layeredPremiumPromoService;

    public GetPremiumPromoHandler(ILayeredService<IPremiumPromoConverter> layeredPremiumPromoService)
    {
        _layeredPremiumPromoService = layeredPremiumPromoService;
    }

    protected override Task<MyTelegram.Schema.Help.IPremiumPromo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetPremiumPromo obj)
    {
        var r = _layeredPremiumPromoService.GetConverter(input.Layer).ToPremiumPromo();
        return Task.FromResult(r);
    }
}
