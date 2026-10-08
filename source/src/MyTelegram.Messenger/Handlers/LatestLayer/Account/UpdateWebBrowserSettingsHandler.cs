
namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class UpdateWebBrowserSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateWebBrowserSettings, MyTelegram.Schema.Account.IWebBrowserSettings>
{
    protected override async Task<MyTelegram.Schema.Account.IWebBrowserSettings> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateWebBrowserSettings obj)
    {
        return new TWebBrowserSettings
        {
            DisplayCloseButton = obj.DisplayCloseButton,
            ExternalExceptions = [],
            InappExceptions = [],
            OpenExternalBrowser = obj.OpenExternalBrowser
        };
    }
}

