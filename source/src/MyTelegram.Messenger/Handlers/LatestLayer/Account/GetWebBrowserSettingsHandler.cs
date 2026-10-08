
namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetWebBrowserSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWebBrowserSettings, MyTelegram.Schema.Account.IWebBrowserSettings>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Account.IWebBrowserSettings> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetWebBrowserSettings obj)
    {
        return Task.FromResult<IWebBrowserSettings>(new TWebBrowserSettings
        {
            DisplayCloseButton = true,
            OpenExternalBrowser = false,
            ExternalExceptions = [],
            InappExceptions = [],
            Hash = 0
        });
    }
}

