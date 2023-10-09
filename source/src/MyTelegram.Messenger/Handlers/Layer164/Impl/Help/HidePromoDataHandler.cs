// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Hide MTProxy/Public Service Announcement information
/// See <a href="https://corefork.telegram.org/method/help.hidePromoData" />
///</summary>
internal sealed class HidePromoDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestHidePromoData, IBool>,
    Help.IHidePromoDataHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestHidePromoData obj)
    {
        throw new NotImplementedException();
    }
}
