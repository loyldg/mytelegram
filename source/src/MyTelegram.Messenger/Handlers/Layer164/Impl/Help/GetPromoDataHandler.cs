// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get MTProxy/Public Service Announcement information
/// See <a href="https://corefork.telegram.org/method/help.getPromoData" />
///</summary>
internal sealed class GetPromoDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPromoData, MyTelegram.Schema.Help.IPromoData>,
    Help.IGetPromoDataHandler
{
    protected override Task<MyTelegram.Schema.Help.IPromoData> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetPromoData obj)
    {
        throw new NotImplementedException();
    }
}
