namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

///<summary>
/// Get Telegram Premium promotion information
/// See <a href="https://corefork.telegram.org/method/help.getPremiumPromo" />
///</summary>
internal sealed class GetPremiumPromoHandler(ILayeredService<IPremiumPromoConverter> layeredPremiumPromoService)
    : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPremiumPromo, MyTelegram.Schema.Help.IPremiumPromo>
{
    protected override Task<MyTelegram.Schema.Help.IPremiumPromo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetPremiumPromo obj)
    {
        var r = layeredPremiumPromoService.GetConverter(input.Layer).ToPremiumPromo();
        return Task.FromResult(r);
    }
}
