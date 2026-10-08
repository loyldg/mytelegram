
namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteWebBrowserSettingsExceptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeleteWebBrowserSettingsExceptions, MyTelegram.Schema.Account.IWebBrowserSettings>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Account.IWebBrowserSettings> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestDeleteWebBrowserSettingsExceptions obj)
    {
        throw new NotImplementedException();
    }
}

