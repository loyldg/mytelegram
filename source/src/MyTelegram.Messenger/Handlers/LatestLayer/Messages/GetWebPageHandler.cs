namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get <a href="https://instantview.telegram.org/">instant view</a> page
/// Possible errors
/// Code Type Description
/// 400 WC_CONVERT_URL_INVALID WC convert URL invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getWebPage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetWebPageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetWebPage, MyTelegram.Schema.Messages.IWebPage>
{
    protected override Task<MyTelegram.Schema.Messages.IWebPage> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetWebPage obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IWebPage>(new MyTelegram.Schema.Messages.TWebPage { Webpage = new TWebPageEmpty(), Users = new(), Chats = new() });
    }
}