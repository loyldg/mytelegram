// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://instantview.telegram.org/">instant view</a> page
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WC_CONVERT_URL_INVALID WC convert URL invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getWebPage" />
///</summary>
internal sealed class GetWebPageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetWebPage, MyTelegram.Schema.IWebPage>,
    Messages.IGetWebPageHandler
{
    protected override Task<MyTelegram.Schema.IWebPage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetWebPage obj)
    {
        throw new NotImplementedException();
    }
}
