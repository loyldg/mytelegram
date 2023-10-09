// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get recently used <code>t.me</code> links.When installing official applications from "Download Telegram" buttons present in <a href="https://t.me/">t.me</a> pages, a referral parameter is passed to applications after installation.<br>
/// If, after downloading the application, the user creates a new account (instead of logging into an existing one), the referral parameter should be imported using this method, which returns the <a href="https://t.me/">t.me</a> pages the user recently opened, before installing Telegram.
/// See <a href="https://corefork.telegram.org/method/help.getRecentMeUrls" />
///</summary>
internal sealed class GetRecentMeUrlsHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetRecentMeUrls, MyTelegram.Schema.Help.IRecentMeUrls>,
    Help.IGetRecentMeUrlsHandler
{
    protected override Task<MyTelegram.Schema.Help.IRecentMeUrls> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetRecentMeUrls obj)
    {
        throw new NotImplementedException();
    }
}
