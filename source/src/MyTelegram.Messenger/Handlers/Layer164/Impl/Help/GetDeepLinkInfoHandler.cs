// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get info about an unsupported deep link, see <a href="https://corefork.telegram.org/api/links#unsupported-links">here for more info »</a>.
/// See <a href="https://corefork.telegram.org/method/help.getDeepLinkInfo" />
///</summary>
internal sealed class GetDeepLinkInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetDeepLinkInfo, MyTelegram.Schema.Help.IDeepLinkInfo>,
    Help.IGetDeepLinkInfoHandler
{
    protected override Task<MyTelegram.Schema.Help.IDeepLinkInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetDeepLinkInfo obj)
    {
        throw new NotImplementedException();
    }
}
