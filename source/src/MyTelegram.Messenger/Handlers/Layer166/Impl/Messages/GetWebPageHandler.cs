// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://instantview.telegram.org/">instant view</a> page
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WC_CONVERT_URL_INVALID WC convert URL invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getWebPage" />
///</summary>
internal sealed class GetWebPageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetWebPage, MyTelegram.Schema.Messages.IWebPage>,
    Messages.IGetWebPageHandler
{
    protected override Task<MyTelegram.Schema.Messages.IWebPage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetWebPage obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IWebPage>(new MyTelegram.Schema.Messages.TWebPage
        {
            Webpage = new TWebPageEmpty(),
            Users = new(),
            Chats = new()
        });
    }
}
