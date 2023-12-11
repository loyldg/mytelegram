// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get MTProxy/Public Service Announcement information
/// See <a href="https://corefork.telegram.org/method/help.getPromoData" />
///</summary>
internal sealed class GetPromoDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPromoData, MyTelegram.Schema.Help.IPromoData>,
    Help.IGetPromoDataHandler
{
    protected override Task<IPromoData> HandleCoreAsync(IRequestInput input,
        RequestGetPromoData obj)
    {
        IPromoData r = new TPromoDataEmpty { Expires = (int)DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() };

        return Task.FromResult(r);
    }
}
